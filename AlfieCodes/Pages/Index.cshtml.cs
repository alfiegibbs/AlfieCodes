namespace AlfieCodes.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class IndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly ILogger<IndexModel> _logger;
        public PaginatedList<BlogPost> BlogPosts { get; private set; }


        public IndexModel( BlogDbContext blogDbContext, ILogger<IndexModel> logger )
        {
            _blogDbContext = blogDbContext;
            _logger = logger;
        }

        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<BlogPost> blogPostsData = from bp in _blogDbContext.BlogPosts orderby bp.CreatedAt select bp;

            int pageSize = 5;
            PaginatedList<BlogPost> paginatedList = BlogPosts = await PaginatedList<BlogPost>.CreateAsync(
                                                                  blogPostsData.AsNoTracking()
                                                                        .OrderByDescending( bp => bp.CreatedAt ), 
                                                                        pageIndex ?? 1, pageSize );
        }
    }
}
