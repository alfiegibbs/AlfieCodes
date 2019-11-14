namespace AlfieCodes.Pages
{
    using AlfieCodes.Data;
    using BCrypt.Net;
    using System.Threading.Tasks;
    using AlfieCodes.Models;
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
        public RegisterRequest RegisterRequest { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        private string HashPassword( string password )
        {
            var hashedPassword = BCrypt.HashPassword( password );
            return hashedPassword;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if ( !ModelState.IsValid )
            {
                return Page();
            }
            
            _blogDbContext.Users.Add( new Data.Users
            {
                Username = RegisterRequest.Username,
                Email = RegisterRequest.Email,
                Password = HashPassword( RegisterRequest.Password )
            } );
            await _blogDbContext.SaveChangesAsync();
            return RedirectToPage( "./Index" );
        }
    }
}
