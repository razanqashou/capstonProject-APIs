﻿using capAPI.DTOs.Request;
using capAPI.DTOs.Responce;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Azure;
using capAPI.Helpers;

namespace capAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //teset
        //hhjh
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginInput input)
        {
            var Response = new LoginOutput();
            try
            {
                if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password))
                    throw new Exception("Invalid email or password");

                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Trust Server Certificate=True;";
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                string query = @"SELECT userID, CONCAT(FirstName, ' ', LastName) AS FullName 
                            FROM USERS 
                           WHERE EMAIL = @email AND PasswordHash = @password AND RoleID = 3";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@email", input.Email);
                command.Parameters.AddWithValue("@password", input.Password);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                    throw new Exception("invalid email / pass");
                if (dt.Rows.Count > 1)
                    throw new Exception("query contains more than one");


                foreach (DataRow row in dt.Rows)
                {
                    Response.Id = Convert.ToInt32(row["userID"]);
                    Response.Name = row["FullName"].ToString();
                }



                return StatusCode(200, $"Welcome {Response.Name}");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }






        [HttpPost]
        [Route("Rest-password")]
        public async Task<IActionResult> RestPassword(RestPasswordInput input)
        {
            var response = new RestPassordOutput();
            try
            {
                if (string.IsNullOrEmpty(input.Email) || !Validation.IsValidEmail(input.Email))
                    throw new Exception("invalid email");

                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Trust Server Certificate=True;";
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                string checkEmailQuery = "SELECT UserID FROM Users WHERE Email = @Email AND RoleID = 3";
                SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, connection);

                checkEmailCmd.CommandType = System.Data.CommandType.Text;
                checkEmailCmd.Parameters.AddWithValue("@Email", input.Email);
                int emailCount = (int)checkEmailCmd.ExecuteScalar();

                if (emailCount == 0)
                    throw new Exception("No such email with RoleID 3.");

                object userIdObj = checkEmailCmd.ExecuteScalar();
                if (userIdObj == null)
                    throw new Exception("No such email with RoleID 3.");

                int userId = Convert.ToInt32(userIdObj);

                string otpCode = OTPHelper.GenerateOTP();

                string insertOtpQuery = @"
                INSERT INTO UserOTPCodes (UserID, OTPCode,ExpiresAt)
                VALUES (@UserID, @OTPCode,DATEADD(HOUR, 1, GETDATE()))";

                SqlCommand insertOtpCmd = new SqlCommand(insertOtpQuery, connection);
                insertOtpCmd.CommandType = System.Data.CommandType.Text;
                insertOtpCmd.Parameters.AddWithValue("@UserID", userId);
                insertOtpCmd.Parameters.AddWithValue("@OTPCode", otpCode);
                insertOtpCmd.ExecuteNonQuery();

                int insertedOtpId = Convert.ToInt32(insertOtpCmd.ExecuteScalar());


                response.UserId = userId;
                response.OTPCode = otpCode;
                return Ok(response);





            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }


        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpInput input)
        {
            var response = new SignUpOutput();

            try
            {
                if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password) ||
                    string.IsNullOrEmpty(input.FullName) || string.IsNullOrEmpty(input.Phone))
                {
                    throw new Exception("All fields are required");
                }

                Validation.IsValidEmail(input.Email);
                Validation.IsValidPhone(input.Phone);
                Validation.IsValidFullName(input.FullName);
                Validation.IsValidPassword(input.Password);
                Validation.IsValidBirthdate(input.Birthdate);

                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Trust Server Certificate=True;";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    using (SqlCommand command = new SqlCommand("sp_RegisterClient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var nameParts = input.FullName.Split(' ');
                        string firstName = nameParts[0];
                        string lastName = nameParts.Length > 1 ? nameParts[1] : "";

                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", input.Email);
                        command.Parameters.AddWithValue("@PasswordHash", input.Password);
                        command.Parameters.AddWithValue("@Phone", input.Phone);
                        command.Parameters.AddWithValue("@Birthdate", input.Birthdate);


                        connection.Open();
                        var userId = command.ExecuteScalar();

                        response.UserId = Convert.ToInt32(userId);
                        response.Message = "User registered successfully";
                    }
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpInput input)
        {
            try
            {
                if (input.UserId <= 0)
                    return BadRequest(new VerifyOtpOutput { Message = "Invalid user ID" });

                if (!Validation.IsValidPassword(input.NewPassword))
                    return BadRequest(new VerifyOtpOutput { Message = "Invalid password format" });

                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Trust Server Certificate=True;";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    await connection.OpenAsync();

                    string otpError;
                    if (!Validation.IsValidOtp(connection, input.UserId, input.OTPCode, out otpError))
                        return BadRequest(new VerifyOtpOutput { Message = otpError });

                    string updatePasswordQuery = "UPDATE Users SET PasswordHash = @Password WHERE UserID = @UserID";
                    using (SqlCommand updatePasswordCmd = new SqlCommand(updatePasswordQuery, connection))
                    {
                        updatePasswordCmd.Parameters.AddWithValue("@Password", input.NewPassword);  // Note: Hashing preferred
                        updatePasswordCmd.Parameters.AddWithValue("@UserID", input.UserId);

                        int rowsAffected = await updatePasswordCmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                            return Ok(new VerifyOtpOutput { Message = "Password updated successfully" });
                        else
                            return BadRequest(new VerifyOtpOutput { Message = "Failed to update password" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new VerifyOtpOutput { Message = ex.Message });
            }
        }
    } }
    



    