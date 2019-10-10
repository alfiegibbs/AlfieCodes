namespace AlfieCodes.Areas.Administration.Pages
{
    using System;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class CreateBlogPostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public CreateBlogPostModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public BlogPost BlogPost { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public string MarkupStringToDataBase(string markup)
        {
            string replace = markup.Replace( Environment.NewLine, "<br/>");

            return replace;
        }

        public async Task<IActionResult> OnPost()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }

            _blogDbContext.BlogPosts.Add( new BlogPost
            {
                CreatedAt = DateTime.Now,
                Title = BlogPost.Title,
                Body = MarkupStringToDataBase( BlogPost.Body )
            } );
            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage( "/Index" );
        }
    }
}
