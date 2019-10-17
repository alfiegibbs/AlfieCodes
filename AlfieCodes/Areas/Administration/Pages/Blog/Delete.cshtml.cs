namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using AlfieCodes.Data;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class DeleteModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public DeleteModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        private BlogPost BlogPost { get; set; }
        [BindProperty]
        public Guid PostId { get; set; }

        public IActionResult OnGet(Guid postId)
        {
            PostId = postId;
            BlogPost = _blogDbContext.BlogPosts.FirstOrDefault( x => x.Id == PostId );
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
                var blogPost = _blogDbContext.BlogPosts.FirstOrDefault( x => x.Id == PostId );

                if ( blogPost != null )
                {
                    _blogDbContext.BlogPosts.RemoveRange( blogPost );
                    await _blogDbContext.SaveChangesAsync();
                }
            }


            return RedirectToPage( "/Index" );
        }
    }
}
