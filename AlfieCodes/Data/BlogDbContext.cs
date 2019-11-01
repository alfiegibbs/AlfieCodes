namespace AlfieCodes.Data
{
    using Microsoft.EntityFrameworkCore;

    public class BlogDbContext : DbContext
    {
        public BlogDbContext( DbContextOptions<BlogDbContext> options )
            : base( options ) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<BlogPostTags> BlogPostTags { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<BlogPostTags>()
                        .HasKey( x => new { x.BlogPostId, x.TagId } );

            modelBuilder.Entity<BlogPostTags>()
                        .HasOne( x => x.BlogPost )
                        .WithMany( x => x.BlogPostTags )
                        .HasForeignKey( x => x.BlogPostId );

            modelBuilder.Entity<BlogPostTags>()
                        .HasOne( x => x.Tag )
                        .WithMany( x => x.BlogPosts )
                        .HasForeignKey( x => x.TagId );
        }
    }
}
