namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<IActionResult> OnPost()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }

            if ( BlogPost.Image == null )
            {
                BlogPost.Image = "https://dummyimage.com/600x400/000/fff&text=Placeholder";
            }

            var summary = String.Join( " ", BlogPost.Body.Split().Take( 50 ) );
            _blogDbContext.BlogPosts.Add( new BlogPost
            {
                CreatedAt = DateTime.Now,
                Title = BlogPost.Title,
                Body = BlogPost.Body,
                Summary = summary,
                Tags = BlogPost.Tags,
                ReadTime = BlogPost.ReadTime,
                Image = BlogPost.Image
            } );

            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage( "/Index" );
        }
    }
}
