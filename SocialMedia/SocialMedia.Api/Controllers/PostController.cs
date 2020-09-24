namespace SocialMedia.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Repositories;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository postRepository;
        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts =  await this.postRepository.GetPost();
            return Ok(posts);
        }
    }
}
