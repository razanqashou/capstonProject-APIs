namespace capAPI.DTOs.Request
{
    public class VerificationInputDTO
    {

        public int Userid{ get; set; }
        public string OTPCode { get; set; }
        public string type { get; set; }

    }
}
