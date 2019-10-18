namespace AlfieCodes.Areas.Administration.Pages.Users
{
    using AlfieCodes.Data;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class DeleteModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public DeleteModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        private Users Users { get; set; }
        [BindProperty]
        public Guid UserId { get; set; }

        public IActionResult OnGet(Guid userId)
        {
            UserId = userId;
            Users = _blogDbContext.Users.FirstOrDefault( x => x.Id == UserId );
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }

            await using ( _blogDbContext )
            {
                var users = _blogDbContext.Users.FirstOrDefault( x => x.Id == UserId );

                if ( users != null )
                {
                    _blogDbContext.Users.RemoveRange( users );
                    await _blogDbContext.SaveChangesAsync();
                }
            }

            return RedirectToPage( "/Index" );
        }
    }
}
