namespace SocialMedia.Infrastructure.Services
{
    using SocialMedia.Core.QueryFilters;
    using SocialMedia.Infrastructure.Interfaces;
    using System;

    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string url = $"{_baseUri}{actionUrl}";
            return new Uri(url);
        }
    }
}
