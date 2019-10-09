namespace AlfieCodes.Pages
{
    using AlfieCodes.Data;
    using BCrypt.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class RegisterModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public RegisterModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public Users Users { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        private string HashPassword( string password )
        {
            var encryptedPassword = BCrypt.HashPassword( password );
            return encryptedPassword;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }

            _blogDbContext.Users.Add( new Users
            {
                Username = Users.Username,
                Email = Users.Email,
                Password = HashPassword( Users.Password )
            } );
            await _blogDbContext.SaveChangesAsync();
            return RedirectToPage( "./Index" );
        }
    }
}
