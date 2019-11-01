namespace AlfieCodes.Areas.Administration.Pages.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class CreateBlogPostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public CreateBlogPostModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public BlogPost BlogPost { get; set; }
        [ BindProperty ]
        public Tags Tags { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }

            string summary = string.Join( " ", BlogPost.Body.Split().Take( 50 ) );

            var blogPost = new BlogPost
            {
                CreatedAt = DateTime.Now,
                Title = BlogPost.Title,
                Body = BlogPost.Body,
                Summary = summary,
                ReadTime = BlogPost.ReadTime,
                Image = BlogPost.Image ?? "https://dummyimage.com/600x400/000/fff&text=Placeholder"
            };
            
            _blogDbContext.BlogPosts.Add( blogPost );

            foreach ( string tagString in Tags.Value.Split( "," ) )
            {
                var tag = _blogDbContext.Tags.FirstOrDefault( x => x.Value == tagString );

                if ( tag == null )
                {
                    tag = new Tags
                    {
                        Id = new Guid(),
                        Value = tagString
                    };

                    _blogDbContext.Tags.Add( tag );
                }

                _blogDbContext.BlogPostTags.Add( new BlogPostTags
                {
                    BlogPost = blogPost,
                    BlogPostId = blogPost.Id,
                    Tag = tag,
                    TagId = tag.Id
                } );
            }

            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage( "/Index" );
        }
    }
}
