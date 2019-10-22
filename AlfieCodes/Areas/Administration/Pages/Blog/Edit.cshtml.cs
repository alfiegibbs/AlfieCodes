namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    public class EditModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public EditModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public BlogPost BlogPost { get; set; }
        [BindProperty]
        public Guid PostId { get; set; }

        public IActionResult OnGet(Guid postId)
        {
            PostId = postId;
            BlogPost = _blogDbContext.BlogPosts.FirstOrDefault( x => x.Id == PostId );
            if ( BlogPost == null )
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }

            await using ( _blogDbContext )
            {
                var blogPost = _blogDbContext.BlogPosts.Find( PostId );

                blogPost.Body = BlogPost.Body;
                blogPost.ReadTime = BlogPost.ReadTime;
                blogPost.Tags = BlogPost.Tags;
                blogPost.Title = BlogPost.Title;
                blogPost.Image = BlogPost.Image;

                _blogDbContext.BlogPosts.Update( blogPost );
                _blogDbContext.SaveChanges();
            }

            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage( "/Index" );
        }
    }
    }