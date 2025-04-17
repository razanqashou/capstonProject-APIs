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

            // Optional: Check allowed domains
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
    }
}
