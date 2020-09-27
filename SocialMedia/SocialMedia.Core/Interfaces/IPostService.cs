namespace SocialMedia.Core.Interfaces
{
    using SocialMedia.Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPost();

        Task<Post> GetPost(int postId);

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);

        Task<bool> DeletePost(int id);
    }
}