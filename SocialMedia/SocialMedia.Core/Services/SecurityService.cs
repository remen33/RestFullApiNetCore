namespace SocialMedia.Core.Services
{
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using System.Threading.Tasks;

    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLogginByCredentials(UserLogin userLogin)
        {
            return await unitOfWork.SecurityRepository.GetLoggingByCredencials(userLogin);
        }


        public async Task RegisterUser(Security security)
        {
            await unitOfWork.SecurityRepository.Add(security);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
