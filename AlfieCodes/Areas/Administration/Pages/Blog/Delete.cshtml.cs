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

        public IReadOnlyCollection<BlogPost> BlogPosts { get; private set; }

        public DeleteModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
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