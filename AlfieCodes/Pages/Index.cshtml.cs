namespace AlfieCodes.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using AlfieCodes.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class IndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly ILogger<IndexModel> _logger;
        public PaginatedList<BlogPost> BlogPosts { get; private set; }
        public List<TagCloudEntry> TagCloud { get; set; }

        public IndexModel( BlogDbContext blogDbContext, ILogger<IndexModel> logger )
        {
            _blogDbContext = blogDbContext;
            _logger = logger;
        }

        public async Task OnGetAsync( int? pageIndex )
        {
            var blogPostsData = from bp in _blogDbContext.BlogPosts
                                orderby bp.CreatedAt
                                select bp;

            int pageSize = 5;
            BlogPosts = await PaginatedList<BlogPost>.CreateAsync(
                                                                  blogPostsData.AsNoTracking()
                                                                               .OrderByDescending( bp => bp.CreatedAt ),
                                                                  pageIndex ?? 1, pageSize );

            var tags = from tag in _blogDbContext.Tags
                       let tagCount = tag.BlogPosts.Count
                       let tagName = tag.Value
                       orderby tagCount descending
                       select new TagCloudEntry( tagName, tagCount );

            TagCloud = await tags.ToListAsync();

        }
    }
}
