namespace AlfieCodes.Areas.Posts.Pages
{
    using System;
    using System.Linq;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class IndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly ILogger<AlfieCodes.Pages.IndexModel> _logger;

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public Guid PostId { get; set; }

        public BlogPost BlogPost { get; private set; }

        public IndexModel(BlogDbContext blogDbContext, ILogger<AlfieCodes.Pages.IndexModel> logger)
        {
            _blogDbContext = blogDbContext;
            _logger = logger;
        }

        public IActionResult OnGet(string title, Guid postId)
        {
            Title = title;
            PostId = postId;
            BlogPost = _blogDbContext.BlogPosts.FirstOrDefault( x => x.Id == PostId );
            if ( BlogPost == null )
            {
                return NotFound();
            }

            return Page();
        }
    }
}
