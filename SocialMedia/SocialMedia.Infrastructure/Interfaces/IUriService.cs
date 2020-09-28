namespace SocialMedia.Infrastructure.Interfaces
{
    using SocialMedia.Core.QueryFilters;
    using System;

    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}