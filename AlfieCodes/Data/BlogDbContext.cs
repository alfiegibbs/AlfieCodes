﻿namespace AlfieCodes.Data
{
    using Microsoft.EntityFrameworkCore;

    public class BlogDbContext : DbContext
    {
        public BlogDbContext( DbContextOptions<BlogDbContext> options )
            : base( options )
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}