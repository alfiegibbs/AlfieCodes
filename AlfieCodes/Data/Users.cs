namespace AlfieCodes.Data
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class Users
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
