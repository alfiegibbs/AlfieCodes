namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class CommentsModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public CommentsModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        public IReadOnlyCollection<Comments> Comments { get; private set; }

        [ BindProperty ]
        public Guid PostId { get; set; }

        public IActionResult OnGet( Guid postId )
        {
            PostId = postId;
            Comments = _blogDbContext.Comments.Where( x => x.ForeignKey == PostId ).ToList();
            if ( Comments == null )
            {
                return NotFound();
            }

            return Page();
        }
    }
}
