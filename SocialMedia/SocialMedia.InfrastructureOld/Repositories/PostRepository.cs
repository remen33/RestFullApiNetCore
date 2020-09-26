namespace SocialMedia.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext context;
        public PostRepository(SocialMediaContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Post>> GetPost()
        {
            var posts =  await this.context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            var post = await this.context.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

            return post;
        }

        public async Task InsertPost(Post post)
        {
            this.context.Posts.Add(post);
            await this.context.SaveChangesAsync();
        }
    }
}
