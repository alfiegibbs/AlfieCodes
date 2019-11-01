namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class EditModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public EditModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public BlogPost BlogPost { get; set; }

        [ BindProperty ]
        public Guid PostId { get; set; }

        [ BindProperty ]
        public string Tags { get; set; }

        public IActionResult OnGet( Guid postId )
        {
            PostId = postId;
            BlogPost = _blogDbContext.BlogPosts
                                     .Include( x => x.BlogPostTags )
                                     .ThenInclude( x => x.Tag )
                                     .FirstOrDefault( x => x.Id == PostId );
            if ( BlogPost == null )
            {
                return NotFound();
            }

            var tagValues = BlogPost.BlogPostTags
                                    .Select( x => x.Tag.Value );
            Tags = tagValues.Any()
                ? tagValues.Aggregate( ( lhs, rhs ) => $"{lhs},{rhs}" )
                : string.Empty;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }


            var blogPost = await _blogDbContext.BlogPosts
                                               .Include( bp => bp.BlogPostTags )
                                               .SingleOrDefaultAsync( bp => bp.Id == PostId );

            if ( blogPost == null )
            {
                return NotFound();
            }

            blogPost.Body = BlogPost.Body;
            blogPost.ReadTime = BlogPost.ReadTime;
            blogPost.Title = BlogPost.Title;
            blogPost.Image = BlogPost.Image;

            blogPost.BlogPostTags.Clear();

            if ( !string.IsNullOrWhiteSpace( Tags ) )
            {
                foreach ( string tagString in Tags.Split( "," ) )
                {
                    var tag = _blogDbContext.Tags.FirstOrDefault( x => x.Value == tagString );

                    if ( tag == null )
                    {
                        tag = new Tags
                        {
                            Id = new Guid(),
                            Value = tagString
                        };

                        _blogDbContext.Tags.Add( tag );
                    }

                    blogPost.BlogPostTags.Add( new BlogPostTags
                    {
                        BlogPost = BlogPost,
                        BlogPostId = blogPost.Id,
                        Tag = tag
                    } );
                }
            }

            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage( "/Index" );
        }
    }
}
