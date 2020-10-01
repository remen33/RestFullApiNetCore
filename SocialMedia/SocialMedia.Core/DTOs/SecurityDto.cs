namespace SocialMedia.Core.DTOs
{
    using SocialMedia.Core.Enumerations;

    public class SecurityDto
    {
        public string  User { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public RoleType? Role { get; set; }
    }
}
