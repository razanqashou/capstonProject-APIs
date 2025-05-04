namespace capAPI.DTOs.Request
{
    public class VerificationInputDTO
    {
        public string Email { get; set; }
        public bool IsSignup { get; set; }
        public string OTPCode { get; set; }

    }
}
