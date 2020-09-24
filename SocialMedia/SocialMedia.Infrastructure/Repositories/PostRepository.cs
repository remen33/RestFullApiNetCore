namespace SocialMedia.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext context;
        public PostRepository(SocialMediaContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Publicacion>> GetPost()
        {
            var posts =  await this.context.Publicacion.ToListAsync();
            return posts;
        }
    }
}
