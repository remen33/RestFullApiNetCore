namespace SocialMedia.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Exceptions;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Core.QueryFilters;

    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;        
        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;            
        }

        public IEnumerable<Post> GetPost(PostQueryFilter filters)
        {
            var posts = this.unitOfWork.PostRepository.GetAll();

            if (filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }

            if (filters.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date.Value.ToShortDateString());
            }

            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            return await this.unitOfWork.PostRepository.GetById(postId);
        }

        public async Task InsertPost(Post post)
        {
            var currentUser =  await this.unitOfWork.UserRepository.GetById(post.UserId);

            if (currentUser == null)
            {
                throw new BusinessException("User doesn't exist");
            }

            var userPosts = await this.unitOfWork.PostRepository.GetPostbyUser(post.UserId);

            if (userPosts != null && userPosts.Count() < 10)
            {
                var lastPost = userPosts.OrderByDescending(q => q.Date).FirstOrDefault();

                if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish the post");
                }
            }

            if(post.Description.ToLower().Contains("sexo"))
            {
                throw new BusinessException("Content not allowed");
            }

            await this.unitOfWork.PostRepository.Add(post);
            await this.unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            this.unitOfWork.PostRepository.Update(post);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await this.unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
