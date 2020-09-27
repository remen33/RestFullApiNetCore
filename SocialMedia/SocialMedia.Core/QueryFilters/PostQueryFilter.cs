namespace SocialMedia.Core.QueryFilters
{
    using System;

    public class PostQueryFilter
    {
        public int? UserId { get; set; }

        public DateTime? Date { get; set; }

        public string Description { get; set; }
    }
}
