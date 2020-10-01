namespace SocialMedia.Core.Interfaces
{
    using SocialMedia.Core.Entities;
    using System.Threading.Tasks;

    public interface ISecurityService
    {
        Task<Security> GetLogginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}