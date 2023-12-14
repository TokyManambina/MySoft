namespace SoftSignAPI.Dto
{
    public class AuthenticationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? SocietyId { get; set; }
    }
}
