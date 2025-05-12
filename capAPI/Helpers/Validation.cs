using System.Globalization;
using System.Text.RegularExpressions;
using capAPI.DTOs.Request.ItemOption;
using capAPI.DTOs.Request.Item;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Components.Forms;
using capAPI.Models;
using static System.Net.Mime.MediaTypeNames;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using System.Linq;


namespace capAPI.Helpers
{
    public static class Validation
    {

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email is required.");

            string pattern = @"^[a-zA-Z0-9._%+-]+@(gmail|yahoo|outlook|hotmail)\.com$";

            if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                throw new Exception("Invalid email format. Only Gmail, Yahoo, Outlook, and Hotmail (.com) are allowed.");

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



        // Method to validate required fields
        public static string ValidateRequiredFieldsItemOption(CreateItemOption input)
        {
            if (string.IsNullOrEmpty(input.NameAr))
            {
                return "NameAr is required.";
            }

            if (string.IsNullOrEmpty(input.NameEn))
            {
                return "NameEn is required.";
            }

            if (input.ItemId <= 0)
            {
                return "ItemId is required and must be greater than 0.";
            }

            if (input.OptionCategoryId <= 0)
            {
                return "OptionCategoryId is required and must be greater than 0.";
            }

            return null; // Return null if no validation errors
        }


        public static List<string> Validate(CreatItemInputDTO dto, IFormFile? imageFile, List<Category> availableCategories)
        {
            var errors = new List<string>();

            // Arabic validation (only Arabic letters + space)
            if (string.IsNullOrWhiteSpace(dto.NameAr) || !Regex.IsMatch(dto.NameAr, @"^[\u0621-\u064A\s]+$"))
                errors.Add("Arabic Name must contain only Arabic letters and spaces.");

            if (!string.IsNullOrWhiteSpace(dto.DescriptionAr) && !Regex.IsMatch(dto.DescriptionAr, @"^[\u0621-\u064A\s]*$"))
                errors.Add("Arabic Description must contain only Arabic letters and spaces.");

            // English validation (only English letters + space)
            if (string.IsNullOrWhiteSpace(dto.NameEn) || !Regex.IsMatch(dto.NameEn, @"^[a-zA-Z\s]+$"))
                errors.Add("English Name must contain only English letters and spaces.");

            if (!string.IsNullOrWhiteSpace(dto.DescriptionEn) && !Regex.IsMatch(dto.DescriptionEn, @"^[a-zA-Z\s]*$"))
                errors.Add("English Description must contain only English letters and spaces.");

            // Price validation
            if (dto.Price <= 0)
                errors.Add("Price must be a positive number greater than 0.");

            // Category validation
            if (!availableCategories.Any(c => c.CategoryId== dto.CategoryId))
                errors.Add("Selected category is not available.");

            // Image validation
            if (imageFile == null || imageFile.Length == 0)
            {
                errors.Add("Image file is required.");
            }
            else
            {
                var extension = Path.GetExtension(imageFile.FileName).ToLower();
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".webp" };

                if (!allowedExtensions.Contains(extension))
                    errors.Add("Image must be .png, .jpg, .jpeg, or .webp.");

                try
                {
                    using var image = SixLabors.ImageSharp.Image.Load(imageFile.OpenReadStream());
                    if (image.Width != 780 || image.Height != 380)
                        errors.Add("Image dimensions must be exactly 780x380 pixels.");
                }
                catch
                {
                    errors.Add("Invalid image file.");
                }
            }

            return errors;
        }

    }

}



