﻿namespace SocialMedia.Core.Interfaces
{
    using SocialMedia.Core.Entities;
    using SocialMedia.Core.QueryFilters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostService
    {
        IEnumerable<Post> GetPost(PostQueryFilter filters);

        Task<Post> GetPost(int postId);

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);

        Task<bool> DeletePost(int id);
    }
}