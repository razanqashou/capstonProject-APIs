namespace capAPI.Helpers
{
    public static class OTPHelper
    {
        public static string GenerateOTP()
        {
            var random = new Random();
            return new string(Enumerable.Repeat("0123456789", 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
