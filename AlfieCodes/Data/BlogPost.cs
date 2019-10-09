namespace AlfieCodes.Data
{
    using System;

    public class BlogPost
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
