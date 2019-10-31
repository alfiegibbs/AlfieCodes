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
        [ BindProperty ]
        public Tags Tags { get; set; }


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

            var summary = String.Join( " ", BlogPost.Body.Split().Take( 50 ) );
            _blogDbContext.BlogPosts.Add( new BlogPost
            {
                Id = new Guid(),
                CreatedAt = DateTime.Now,
                Title = BlogPost.Title,
                Body = BlogPost.Body,
                Summary = summary,
                ReadTime = BlogPost.ReadTime,
                Image = BlogPost.Image ?? "https://dummyimage.com/600x400/000/fff&text=Placeholder"
            } );

            _blogDbContext.Tags.Add( new Tags
            {
                Id = new Guid(),
                ForeignKey = BlogPost.Id,
                Value = BlogPost.Tags
            } );

            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage( "/Index" );
        }
    }
}
