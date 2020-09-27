namespace SocialMedia.Core.Interfaces
{
    using SocialMedia.Core.Entities;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User> GetUser(int userId);
    }
}
