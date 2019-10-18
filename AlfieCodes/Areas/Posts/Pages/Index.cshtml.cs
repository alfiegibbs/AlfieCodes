namespace AlfieCodes.Areas.Posts.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using AlfieCodes.Models;
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
        [BindProperty]
        public CommentRequest CommentRequest { get; set; }
        public IReadOnlyCollection<Comments> Comments { get; private set; }


        public BlogPost BlogPost { get; private set; }


        public IndexModel(BlogDbContext blogDbContext, ILogger<AlfieCodes.Pages.IndexModel> logger)
        {
            _blogDbContext = blogDbContext;
            _logger = logger;
        }

        public IActionResult OnGet(string title, Guid postId)
        {
            var commentList = _blogDbContext.Comments.Where( x => x.ForeignKey == postId ).ToList();
            commentList.Reverse();
            Comments = commentList;


            Title = title;
            PostId = postId;
            BlogPost = _blogDbContext.BlogPosts.FirstOrDefault( x => x.Id == PostId );
            if ( BlogPost == null )
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }

            _blogDbContext.Comments.Add( new Comments
                {
                    ForeignKey = PostId,
                    Id = new Guid(),
                    Body = CommentRequest.Body,
                    Username = User.Identity.Name ?? "Anonymous",
                    DateTime = DateTime.Now
                });
            await _blogDbContext.SaveChangesAsync();
            return RedirectToPage( "" );
        }
    }
}
