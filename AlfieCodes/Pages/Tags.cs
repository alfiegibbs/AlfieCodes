using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlfieCodes.Pages
{
    using AlfieCodes.Data;
    using Microsoft.EntityFrameworkCore;

    public class TagsModel : PageModel
    {
        private const int PageSize = 5;
        private readonly BlogDbContext _blogDbContext;
        public PaginatedList<BlogPost> BlogPosts { get; private set; }


        public TagsModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public string Tag { get; private set; }


        public async Task OnGetAsync( string tag, int? pageIndex )
        {
            Tag = tag;
            var blogPostsData = from blogPost in _blogDbContext.BlogPosts
                                where blogPost.BlogPostTags.Any( x => x.Tag.Value == tag )
                                orderby blogPost.CreatedAt
                                select blogPost;

            BlogPosts = await PaginatedList<BlogPost>.CreateAsync( blogPostsData.AsNoTracking()
                                                                                .OrderByDescending( bp => bp.CreatedAt ),
                                                                   pageIndex ?? 1, PageSize );
        }
    }
}
