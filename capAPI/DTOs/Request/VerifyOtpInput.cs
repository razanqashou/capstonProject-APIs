namespace capAPI.DTOs.Request
{
    public class VerifyOtpInput
    {
        public int UserId { get; set; }
        public string OTPCode { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
