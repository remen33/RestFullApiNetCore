namespace SocialMedia.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Infrastructure.Repositories;

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
        public IActionResult GetPosts()
        {
            var posts = this.postRepository.GetPost();
            return Ok(posts);
        }
    }
}
