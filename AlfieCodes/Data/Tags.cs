namespace AlfieCodes.Data
{
    using System;
    using System.Collections.Generic;

    public class Tags
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public IList<BlogPostTags> BlogPosts { get; set; }
    }
}
