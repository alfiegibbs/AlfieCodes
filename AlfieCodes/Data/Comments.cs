namespace AlfieCodes.Data
{
    using System;

    public class Comments
    {
        public Guid Id { get; set; }
        public Guid ForeignKey { get; set; }
        public string Username { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
    }
}
