using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using AlfieCodes.Data;
    using Microsoft.Extensions.Logging;

    public class IndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly ILogger<AlfieCodes.Pages.IndexModel> _logger;


        public IReadOnlyCollection<BlogPost> BlogPosts { get; private set; }

        public IndexModel(BlogDbContext blogDbContext, ILogger<AlfieCodes.Pages.IndexModel> logger)
        {
            _blogDbContext = blogDbContext;
            _logger = logger;
        }

        public void OnGet()
        {
            // Sticks newest blog post on top
            var postList = _blogDbContext.BlogPosts.ToList();
            postList.Reverse();
            BlogPosts = postList;
        }
    }
}