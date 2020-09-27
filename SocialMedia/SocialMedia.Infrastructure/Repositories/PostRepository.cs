namespace SocialMedia.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext socialMediaContext): base(socialMediaContext) { }

        public async Task<IEnumerable<Post>> GetPostbyUser(int userId)
        {
            return await this.entities.Where(q => q.UserId == userId).ToListAsync();
        }
    }
}
