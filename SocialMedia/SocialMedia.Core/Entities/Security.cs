namespace SocialMedia.Core.Entities
{
    using SocialMedia.Core.Enumerations;

    public class Security: BaseEntity
    {
        public int Id { get; set; }
        public string User { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public RoleType Role { get; set; }
    }
}
