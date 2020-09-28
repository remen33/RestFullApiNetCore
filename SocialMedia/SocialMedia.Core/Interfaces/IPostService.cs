namespace SocialMedia.Core.Interfaces
{
    using SocialMedia.Core.CustomEntities;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.QueryFilters;
    using System.Threading.Tasks;

    public interface IPostService
    {
        PagedList<Post> GetPost(PostQueryFilter filters);

        Task<Post> GetPost(int postId);

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);

        Task<bool> DeletePost(int id);
    }
}