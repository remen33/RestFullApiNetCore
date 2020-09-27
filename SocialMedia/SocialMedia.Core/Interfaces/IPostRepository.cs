namespace SocialMedia.Core.Interfaces
{
    using SocialMedia.Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostbyUser(int userId);
    }
}
