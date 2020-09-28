namespace SocialMedia.Core.Services
{
    using Microsoft.Extensions.Options;
    using SocialMedia.Core.CustomEntities;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Exceptions;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Core.QueryFilters;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly PaginationOptions paginationOptions;
        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            this.unitOfWork = unitOfWork;
            this.paginationOptions = options.Value;
        }

        public PagedList<Post> GetPost(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? paginationOptions.DefaultPageSize : filters.PageSize;

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

            var pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);

            return pagedPosts;
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
