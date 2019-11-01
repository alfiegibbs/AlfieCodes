namespace AlfieCodes.Data
{
    using System;

    public class BlogPostTags
    {
        public BlogPost BlogPost { get; set; }
        public Tags Tag { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid TagId { get; set; } 
    }
}
