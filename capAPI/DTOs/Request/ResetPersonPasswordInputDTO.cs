namespace capAPI.DTOs.Request
{
    public class ResetPersonPasswordInputDTO
    {
        public int userid { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
