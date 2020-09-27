﻿namespace SocialMedia.Infrastructure.Repositories
{
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Data;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext socialMediaContext;
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Comment> commentRepository;

        public UnitOfWork(SocialMediaContext socialMediaContext)
        {
            this.socialMediaContext = socialMediaContext;
        }
        public IRepository<Post> PostRepository => postRepository ?? new BaseRepository<Post>(this.socialMediaContext);

        public IRepository<User> UserRepository => userRepository ?? new BaseRepository<User>(this.socialMediaContext);

        public IRepository<Comment> CommentRepository => commentRepository ?? new BaseRepository<Comment>(this.socialMediaContext);

        public void Dispose()
        {
            if (socialMediaContext != null)
            {
                socialMediaContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            socialMediaContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await socialMediaContext.SaveChangesAsync();
        }
    }
}
