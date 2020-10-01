namespace SocialMedia.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using SocialMedia.Api.Response;
    using SocialMedia.Core.CustomEntities;
    using SocialMedia.Core.DTOs;
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.Interfaces;
    using SocialMedia.Core.QueryFilters;
    using SocialMedia.Infrastructure.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;
        private readonly IUriService uriService;

        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            this.postService = postService;
            this.mapper = mapper;
            this.uriService = uriService;
        }

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<PostDto>))]
        public IActionResult GetPosts([FromQuery] PostQueryFilter filters)
        {
            var posts = this.postService.GetPost(filters);
            var postsDto = this.mapper.Map<IEnumerable<PostDto>>(posts);

            var metada = new Metadata()
            {
                TotalPages = posts.TotalPages,
                PageSize = posts.PageSize,
                CurrrentPage = posts.CurrrentPage,
                TotalCount = posts.TotalCount,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = this.uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),
                PreviousPageUrl = this.uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto)
            {
                Metadata = metada
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metada));
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<PostDto>))]
        public async Task<IActionResult> GetPosts(int id)
        {

            var posts = await this.postService.GetPost(id);
            var postsDto = this.mapper.Map<PostDto>(posts);
            var response = new ApiResponse<PostDto>(postsDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = this.mapper.Map<Post>(postDto);
            await this.postService.InsertPost(post);
            postDto = this.mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto postDto)
        {
            var post = this.mapper.Map<Post>(postDto);
            post.Id = id;
            var result = await this.postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
