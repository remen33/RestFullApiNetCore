namespace SocialMedia.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;

    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;        
        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;            
        }

        public async Task<IEnumerable<Post>> GetPost()
        {
            return await this.unitOfWork.PostRepository.GetAll();
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
                throw new System.Exception("User doesn't exist");
            }

            if(post.Description.ToLower().Contains("sexo"))
            {
                throw new System.Exception("Content not allowed");
            }

            await this.unitOfWork.PostRepository.Add(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await this.unitOfWork.PostRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await this.unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
