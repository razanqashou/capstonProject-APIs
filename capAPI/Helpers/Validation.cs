using System.Globalization;
using System.Text.RegularExpressions;

namespace capAPI.Helpers
{
    public static class Validation
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email is required");

            int atIndex = email.IndexOf('@');
            int dotIndex = email.LastIndexOf('.');

            if (atIndex < 1 || dotIndex < atIndex + 2 || dotIndex >= email.Length - 2)
                throw new Exception("Email format is invalid");

            string domain = email.Substring(atIndex + 1, dotIndex - atIndex - 1);
            string extension = email.Substring(dotIndex + 1);

            if (domain.Length < 2 || extension.Length < 2)
                throw new Exception("Email domain is invalid");

            string localPart = email.Substring(0, atIndex);
            foreach (char c in localPart)
            {
                if (!(char.IsLetterOrDigit(c) || c == '.' || c == '-' || c == '_' || c == '+' || c == '%'))
                {
                    throw new Exception("Invalid characters in email");
                }
            }

      
            string fullDomain = domain + "." + extension;
            string[] allowedDomains = { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com" };
            if (!allowedDomains.Contains(fullDomain.ToLower()))
            {
                throw new Exception("Only Gmail, Yahoo, Outlook, or Hotmail are allowed");
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
    }
}


