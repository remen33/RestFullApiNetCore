namespace SocialMedia.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Data;
    using System.Threading.Tasks;

    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context) : base(context) { }

        public async Task<Security> GetLoggingByCredencials(UserLogin login)
        {
            return await this.entities.FirstOrDefaultAsync(x => x.User == login.User && x.Password == login.Password);
        }
    }
}
