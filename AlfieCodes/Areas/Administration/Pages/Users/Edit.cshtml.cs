namespace AlfieCodes.Areas.Administration.Pages.Users
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    public class EditModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public EditModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public Users Users { get; set; }
        [BindProperty]
        public Guid UserId { get; set; }

        public IActionResult OnGet(Guid userId)
        {
            UserId = userId;
            Users = _blogDbContext.Users.FirstOrDefault( x => x.Id == UserId );
            if ( Users == null )
            {
                return NotFound();
            }
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
                var user = _blogDbContext.Users.Find( UserId );

                user.Id = Users.Id;
                user.Email = Users.Email;
                user.Username = Users.Username;
                user.IsAdmin = Users.IsAdmin;

                _blogDbContext.Users.Update( user );
                _blogDbContext.SaveChanges();
            }

            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage( "/Index" );
        }
    }
    }