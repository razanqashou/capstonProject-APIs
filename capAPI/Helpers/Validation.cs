using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace capAPI.Helpers
{
    public static class Validation
    {

        public static bool IsValidEmail(string email)
        {
            
            if (string.IsNullOrWhiteSpace(email))
                return false;

            
            if (email.Length > 254)
                return false;

            
            var parts = email.Split('@');
            if (parts.Length != 2)
                return false;

            string localPart = parts[0];
            string domain = parts[1].ToLower();

            
            if (localPart.Length == 0 || localPart.Length > 64)
                return false;

            
            var allowedDomains = new[] { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com" };
            if (!allowedDomains.Contains(domain))
                return false;

         
            if (localPart.StartsWith('.') || localPart.EndsWith('.'))
                return false;

           
            if (localPart.Contains(".."))
                return false;

           
            foreach (char c in localPart)
            {
                if (!(char.IsLetterOrDigit(c)
                      || c == '.' || c == '-' || c == '_'
                      || c == '+' || c == '%'))
                {
                    return false;
                }
            }

            return true;
        }


        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6)
                throw new Exception("Password is required and must be at least 6 characters");

            if (!password.Any(char.IsUpper))
                throw new Exception("Password must contain at least one uppercase letter");

            if (!password.Any(char.IsLower))
                throw new Exception("Password must contain at least one lowercase letter");

            if (!password.Any(char.IsDigit))
                throw new Exception("Password must contain at least one digit");

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
                throw new Exception("Password must contain at least one special character");

            return true;
        }

        public static bool IsValidBirthdate(string birthdateString)
        {
            if (string.IsNullOrWhiteSpace(birthdateString))
                throw new Exception("Birthdate is required");

            if (!DateTime.TryParseExact(birthdateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthdate))
                throw new Exception("Birthdate must be in the format yyyy-MM-dd");

            var sixteenYearsAgo = DateTime.Today.AddYears(-16);
            if (birthdate > sixteenYearsAgo)
                throw new Exception("User must be at least 16 years old");

            return true;
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new Exception("Phone number is required");


            if (!Regex.IsMatch(phone, @"^07\d{8}$"))
                throw new Exception("Phone number must start with 07 and be 10 digits long");

            return true;
        }

        public static bool IsValidFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new Exception("Full name is required");


            var regex = new Regex(@"^[\p{L} ]+$");
            if (!regex.IsMatch(fullName))
                throw new Exception("Full name must contain only Arabic or English letters and spaces");

            return true;
        }
        public static bool IsValidOtp(SqlConnection connection, int userId, string inputOtp, out string errorMessage)
        {
            errorMessage = "";

            string checkOtpQuery = @"
            SELECT TOP 1 OTPCode, ExpiresAt 
            FROM UserOTPCodes 
            WHERE UserID = @UserID 
            ORDER BY ExpiresAt DESC";

            using (SqlCommand cmd = new SqlCommand(checkOtpQuery, connection))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        errorMessage = "OTP not found or expired";
                        return false;
                    }

                    string storedOtp = reader["OTPCode"].ToString();
                    DateTime expiresAt = Convert.ToDateTime(reader["ExpiresAt"]);

                    if (storedOtp != inputOtp)
                    {
                        errorMessage = "Invalid OTP code";
                        return false;
                    }

                    if (expiresAt < DateTime.Now)
                    {
                        errorMessage = "OTP code has expired";
                        return false;
                    }

                    return true;
                }

            }
        }
    }
}


