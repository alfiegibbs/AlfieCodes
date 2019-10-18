namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using AlfieCodes.Data;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class DeleteCommentsModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public DeleteCommentsModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        private Comments Comments { get; set; }
        [BindProperty]
        public Guid PostId { get; set; }

        public IActionResult OnGet(Guid postId)
        {
            PostId = postId;
            Comments = _blogDbContext.Comments.FirstOrDefault( x => x.Id == PostId );
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
                var comment = _blogDbContext.Comments.FirstOrDefault( x => x.Id == PostId );

                if ( comment != null )
                {
                    _blogDbContext.Comments.RemoveRange( comment );
                    await _blogDbContext.SaveChangesAsync();
                }
            }

            return RedirectToPage( "/Index" );
        }
    }
}
