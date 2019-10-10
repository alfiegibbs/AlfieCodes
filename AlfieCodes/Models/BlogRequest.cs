namespace AlfieCodes.Models
{
    using System;

    public class BlogRequest
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
