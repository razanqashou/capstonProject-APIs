using capAPI.DTOs.Request;
using capAPI.DTOs.Responce;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Azure;
using capAPI.Helpers;
using capAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace capAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DBCapstoneContext _context;
        private readonly string _connectionString;
        public AuthController(DBCapstoneContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginInput input)
        {
            var response = new SignUpOutput();

            try
            {
                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";


                if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password))
                    throw new Exception("Invalid email or password");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();


                    string selectQuery = "SELECT TOP 1 * FROM USERS WHERE Email = @Email AND PasswordHash = @Password AND IsLoggedIn = 0 AND RoleID = 3";
                    SqlCommand selectCommand = new SqlCommand(selectQuery, connection);

                    selectCommand.Parameters.AddWithValue("@Email", input.Email);
                    selectCommand.Parameters.AddWithValue("@Password", input.Password);

                    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                        throw new Exception("invalid email / pass");
                    if (dt.Rows.Count > 1)
                        throw new Exception("query contains more than one");

                    int userId = Convert.ToInt32(dt.Rows[0]["userID"]);


                    Random random = new Random();
                    var otp = random.Next(1111, 9999).ToString();
                    DateTime now = DateTime.Now;
                    DateTime expiresAt = now.AddMinutes(60);


                    string insertOtpQuery = @"
                           INSERT INTO UserOTPCodes (UserID, OTPCode, CreatedAt, CreatedBy, ExpiresAt, IsUsed, isActive)
                       VALUES (@UserID, @OTPCode, @CreatedAt, @CreatedBy, @ExpiresAt, 0, 1)";


                    SqlCommand insertCommand = new SqlCommand(insertOtpQuery, connection);
                    insertCommand.Parameters.AddWithValue("@UserID", userId);
                    insertCommand.Parameters.AddWithValue("@OTPCode", otp);
                    insertCommand.Parameters.AddWithValue("@CreatedAt", now);
                    insertCommand.Parameters.AddWithValue("@CreatedBy", "System");
                    insertCommand.Parameters.AddWithValue("@ExpiresAt", expiresAt);

                    await insertCommand.ExecuteNonQueryAsync();
                    try
                    {
                        await EmailHelper.SendOtpEmail(input.Email, "Your One-Time Sign-In Code", "We received a request to sign in to your account. Please use the code below to complete your sign-in process.", otp);
                    }
                    catch (Exception emailEx)
                    {

                        throw new Exception("Registration failed during email sending: " + emailEx.Message);
                    }

                    response.UserId = userId;
                    response.Message = "  Please check your email OTP has been sent!";

                    return StatusCode(200, response);
                }


            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost]
        [Route("Send-OTP")]
        public async Task<IActionResult> SendOTP([FromBody] string email)

        {
            var response = new SignUpOutput();
            try
            {
                //   string conn = "Server=MSI\\SQLEXPRESS13;Database=lastupdateCapstonDB.bacpac;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                if (string.IsNullOrEmpty(email) || !Validation.IsValidEmail(email))
                    throw new Exception("Invalid email");
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string getUserIdQuery = "SELECT UserID FROM Users WHERE Email = @Email ";
                    SqlCommand getUserIdCmd = new SqlCommand(getUserIdQuery, connection);
                    getUserIdCmd.Parameters.AddWithValue("@Email", email);
                    var result = await getUserIdCmd.ExecuteScalarAsync();

                    if (result == null)
                        throw new Exception("User not found.");

                    int userId = Convert.ToInt32(result);

                    Random random = new Random();
                    var otp = random.Next(1111, 9999).ToString();
                    DateTime now = DateTime.Now;
                    DateTime expiresAt = now.AddMinutes(60);


                    string insertOtpQuery = @"
                           INSERT INTO UserOTPCodes (UserID, OTPCode, CreatedAt, CreatedBy, ExpiresAt, IsUsed, isActive)
                       VALUES (@UserID, @OTPCode, @CreatedAt, @CreatedBy, @ExpiresAt, 0, 1)";


                    SqlCommand insertCommand = new SqlCommand(insertOtpQuery, connection);
                    insertCommand.Parameters.AddWithValue("@UserID", userId);
                    insertCommand.Parameters.AddWithValue("@OTPCode", otp);
                    insertCommand.Parameters.AddWithValue("@CreatedAt", now);
                    insertCommand.Parameters.AddWithValue("@CreatedBy", "System");
                    insertCommand.Parameters.AddWithValue("@ExpiresAt", expiresAt);

                    await insertCommand.ExecuteNonQueryAsync();
                    try
                    {
                        await EmailHelper.SendOtpEmail(email, "Your One-Time Rest-Password Code", "We received a request to Rest-Password to your account. Please use the code below to complete your Rest-Password process.", otp);

                    }
                    catch (Exception emailEx)
                    {

                        throw new Exception("Registration failed during email sending: " + emailEx.Message);
                    }
                    response.UserId= userId;
                    response.Message = "OTP sent successfully";

                }

                    return StatusCode(200, response);


               

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }



        }


[HttpPost]
[Route("Rest-password")]
public async Task<IActionResult> RestPassword(ResetPersonPasswordInputDTO input)
{

    try
    {

        var user = _context.Users.Where(u => u.UserId == input.userid && u.IsVerified==true
         ).SingleOrDefault();
        if (user == null)
        {
            return Ok("user not found");
        }

        Validation.IsValidPassword(input.Password);
        if (input.Password != input.ConfirmPassword)
        {
            return Ok(" Cofirrm password Not Match the password");
        }


        user.PasswordHash = input.ConfirmPassword;


        _context.Update(user);
        _context.SaveChanges();

        return Ok("Congratulations Your password Reset Sesccefully");



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
                if (string.IsNullOrEmpty(input.Password) ||
                    string.IsNullOrEmpty(input.FullName) || string.IsNullOrEmpty(input.Phone))
                {
                    throw new Exception("All fields are required");
                }

 
                Validation.IsValidEmail(input.Email);
                Validation.IsValidPhone(input.Phone);
                Validation.IsValidFullName(input.FullName);
                Validation.IsValidPassword(input.Password);
                Validation.IsValidBirthdate(input.Birthdate);
                //string conn = "Server=MSI\\SQLEXPRESS13;Database=DatabaseEdit;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";


                // string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();


                    using (SqlCommand checkEmailCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", connection))
                    {
                        checkEmailCmd.Parameters.AddWithValue("@Email", input.Email);
                        int existingCount = (int)await checkEmailCmd.ExecuteScalarAsync();
                        if (existingCount > 0)
                            throw new Exception("Email already registered");
                    }

                    using (SqlCommand command = new SqlCommand("sp_RegisterClient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        var nameParts = input.FullName.Split(' ');
                        string firstName = nameParts[0];
                        string lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : "";

                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", input.Email);
                        command.Parameters.AddWithValue("@PasswordHash", input.Password);
                        command.Parameters.AddWithValue("@Phone", input.Phone);
                        command.Parameters.AddWithValue("@Birthdate", input.Birthdate);

                        int userId = Convert.ToInt32(await command.ExecuteScalarAsync());

                        Random random = new Random();
                        var otp = random.Next(1111, 9999).ToString();
                        DateTime now = DateTime.Now;
                        DateTime expiresAt = now.AddMinutes(60);

                        string insertOtpQuery = @"
                    INSERT INTO UserOTPCodes (UserID, OTPCode, CreatedAt, CreatedBy, ExpiresAt, IsUsed, isActive)
                    VALUES (@UserID, @OTPCode, @CreatedAt, @CreatedBy, @ExpiresAt, 0, 1)";

                        SqlCommand insertCommand = new SqlCommand(insertOtpQuery, connection);
                        insertCommand.Parameters.AddWithValue("@UserID", userId);
                        insertCommand.Parameters.AddWithValue("@OTPCode", otp);
                        insertCommand.Parameters.AddWithValue("@CreatedAt", now);
                        insertCommand.Parameters.AddWithValue("@CreatedBy", "System");
                        insertCommand.Parameters.AddWithValue("@ExpiresAt", expiresAt);

                        await insertCommand.ExecuteNonQueryAsync();

                        try
                        {
                            await EmailHelper.SendOtpEmail(input.Email, "Your One-Time Sign-Up Code", "We received a request to sign Up to your account. Please use the code below to complete your sign-upn process.", otp);
                          
                        }
                        catch (Exception emailEx)
                        {

                            throw new Exception("Registration failed during email sending: " + emailEx.Message);
                        }
                      
                        response.UserId = userId;
                        response.Message = "Verifying your email using OTP";

                    }

                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = ex.Message });
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Verification(VerificationInputDTO input)
        {
            try
            {


                var user = await _context.Users
                    .SingleOrDefaultAsync(u => u.UserId == input.Userid);
                
                var otpEntry = await _context.UserOtpcodes
                    .Where(o =>o.Otpcode == input.OTPCode && o.ExpiresAt > DateTime.Now)
                    .OrderByDescending(o => o.ExpiresAt) 
                    .FirstOrDefaultAsync();
           
                if (otpEntry == null)
                    return NotFound("OTP not found or expired");
               



              
                if (input.type == "SignUP")
                {
                    
                   user.IsVerified= true;
                    otpEntry.Otpcode = null;
                    otpEntry.ExpiresAt = null;
                    
                    _context.Update(user);
                    _context.Update(otpEntry);
                    await _context.SaveChangesAsync();

                    return Ok("Your Account Is Verified");
                }
                else if (input.type == "ResetPassword")
                {

                    
                    otpEntry.Otpcode = null;
                    otpEntry.ExpiresAt = null;
                    user.IsVerified = true;
                    _context.Update(user);
                    _context.Update(otpEntry);
                    await _context.SaveChangesAsync();

                    return Ok("OTP Verified Secceccfully");
                }
                else if (input.type == "Login")
                {
                    user.LastLoginTime = DateTime.Now;
                    user.IsLoggedIn = true;
                    otpEntry.Otpcode = null;
                    otpEntry.IsActive = false; 
                    otpEntry.ExpiresAt = null;
                    _context.Update(user);
                    _context.Update(otpEntry);
                    await _context.SaveChangesAsync();

                    

                    var jwtToken = TokenHelper.GenersteJWTToken(user.UserId.ToString(),user.RoleId.ToString());
                    
                    return Ok(jwtToken);
                }
                else return StatusCode(500, "");
            }
            catch (Exception ex)
            {

                var errorMessage = new StringBuilder();
                errorMessage.AppendLine($"Message: {ex.Message}");

                var inner = ex.InnerException;
                while (inner != null)
                {
                    errorMessage.AppendLine($"Inner Exception: {inner.Message}");
                    inner = inner.InnerException;
                }

                return StatusCode(500, errorMessage.ToString());
            }
        }

    }
}

