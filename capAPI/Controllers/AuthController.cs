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
        public AuthController(DBCapstoneContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginInput input)
        {

            try
            {
                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";


                if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password))
                    throw new Exception("Invalid email or password");

                using (SqlConnection connection = new SqlConnection(conn))
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


                    return StatusCode(200, "  Please check your email OTP has been sent!");
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
            try
            {
                string conn = "Server=MSI\\SQLEXPRESS13;Database=lastupdateCapstonDB.bacpac;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

                if (string.IsNullOrEmpty(email) || !Validation.IsValidEmail(email))
                    throw new Exception("Invalid email");
                using (SqlConnection connection = new SqlConnection(conn))
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

                    return Ok("OTP sent successfully");


                }





            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        


        }

        //razan
        [HttpPost]
        [Route("Rest-password")]
        public async Task<IActionResult> RestPassword(RestPasswordInput input)
        {
            var response = new RestPassordOutput();

            try
            {
                if (string.IsNullOrEmpty(input.Email) || !Validation.IsValidEmail(input.Email))
                    throw new Exception("Invalid email");

                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    string checkEmailQuery = "SELECT UserID FROM Users WHERE Email = @Email AND RoleID = 3";
                    SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, connection);
                    checkEmailCmd.Parameters.AddWithValue("@Email", input.Email);

                    object userIdObj = checkEmailCmd.ExecuteScalar();
                    if (userIdObj == null)
                        throw new Exception("No such email with RoleID 3.");

                    int userId = Convert.ToInt32(userIdObj);
                    //    string otpCode = OTPHelper.GenerateOTP();

                    //    string insertOtpQuery = @"
                    //INSERT INTO UserOTPCodes (UserID, OTPCode, ExpiresAt)
                    //VALUES (@UserID, @OTPCode, DATEADD(HOUR, 1, GETDATE()))";

                    //    SqlCommand insertOtpCmd = new SqlCommand(insertOtpQuery, connection);
                    //    insertOtpCmd.Parameters.AddWithValue("@UserID", userId);
                    //    insertOtpCmd.Parameters.AddWithValue("@OTPCode", otpCode);

                    //    int result = insertOtpCmd.ExecuteNonQuery();

                    //    if (result > 0)
                    //    {
                    //        response.UserId = userId;
                    //        response.OTPCode = otpCode;
                    //        return StatusCode(201, response); 
                    //    }
                    //    else
                    //    {
                    return StatusCode(400, "Failed to send OTP code");
                    //}
                }
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

                string conn = "Data Source=DESKTOP-CBGCB75;Initial Catalog=DBCapstone;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(conn))
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
                // الحصول على الـ User بناءً على الإيميل
                var user = await _context.Users
                    .SingleOrDefaultAsync(u => u.Email == input.Email);

                if (user == null)
                    return NotFound("User not found");

                // الحصول على الـ OTP المناسب بناءً على الـ UserId
                var otpEntry = await _context.UserOtpcodes
                    .Where(o => o.UserId == user.UserId && o.Otpcode == input.OTPCode && o.ExpiresAt > DateTime.Now)
                    .OrderByDescending(o => o.ExpiresAt) // إذا كان هناك أكثر من OTP، نأخذ الأحدث
                    .FirstOrDefaultAsync();
           
                if (otpEntry == null)
                    return NotFound("OTP not found or expired");
                if (user.IsLoggedIn==true )
                    return BadRequest("User is already logged in");



                // بناءً على الـ IsSignup من UserOtpcode نقوم بالتحديث
                if (input.IsSignup == true)
                {
                    
                    user.IsVerified = true;
  
                    otpEntry.ExpiresAt = null; 

                    _context.Update(user);
                    _context.Update(otpEntry);
                    await _context.SaveChangesAsync();

                    return Ok("Your Account Is Verified");
                }
                else
                {
                    user.LastLoginTime = DateTime.Now;
                    user.IsLoggedIn = true;
                    otpEntry.IsActive = false; // إلغاء تفعيل OTP بعد تسجيل الدخول
                    otpEntry.ExpiresAt = null;
                    _context.Update(user);
                    _context.Update(otpEntry);
                    await _context.SaveChangesAsync();

                    var jwtToken = TokenHelper.GenersteJWTToken(user.UserId.ToString(),user.RoleId.ToString());
                    
                    return Ok(jwtToken);
                }
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

