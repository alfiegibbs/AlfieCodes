using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlfieCodes.Pages
{
    using AlfieCodes.Data;
    using Microsoft.EntityFrameworkCore;

    public class SearchModel : PageModel
    {
        private const int PageSize = 5;
        private readonly BlogDbContext _blogDbContext;
        public PaginatedList<BlogPost> BlogPosts { get; private set; }


        public SearchModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public string Query { get; private set; }


        public async Task OnGetAsync( string query, int? pageIndex )
        {
            Query = query;
            var blogPostsData = from blogPost in _blogDbContext.BlogPosts
                                where string.IsNullOrWhiteSpace( query ) || 
                                      ( blogPost.Body.Contains( query ) || blogPost.Title.Contains( query ) )
                                orderby blogPost.CreatedAt
                                select blogPost;

            BlogPosts = await PaginatedList<BlogPost>.CreateAsync( blogPostsData.AsNoTracking()
                                                                                .OrderByDescending( bp => bp.CreatedAt ),
                                                                   pageIndex ?? 1, PageSize );
        }
    }
}
