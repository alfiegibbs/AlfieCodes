using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using AlfieCodes.Data;


    public class DeleteModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public DeleteModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        private BlogPost BlogPost { get; set; }
        private Guid PostId { get; set; }

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
                var blogPost = _blogDbContext.BlogPosts.Where( x => x.Id == PostId );

                foreach ( var post in blogPost ) _blogDbContext.Remove( post );
                _blogDbContext.SaveChanges();
            }

            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage( "/Index" );
        }
    }
}
