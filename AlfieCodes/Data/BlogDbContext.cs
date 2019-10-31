namespace AlfieCodes.Data
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
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Tags> Tags { get; set; }
    }
}
