namespace AlfieCodes.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Security.Claims;
    using AlfieCodes.Data;
    using AlfieCodes.Models;
    using BCrypt.Net;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;

    public class LoginModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public LoginModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public LoginRequest LoginRequest { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var user = _blogDbContext.Users.FirstOrDefault( obj => obj.Email == LoginRequest.Email );

            if ( user == null )
            {
                return Redirect( "/Fail" );;
            }

            bool hashedPassword = BCrypt.Verify( LoginRequest.Password, user.Password );

            if ( !hashedPassword )
            {
                return Redirect( "/Fail" );
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            };

            if ( user.IsAdmin )
            {
                claims.Add( new Claim( ClaimTypes.Role, "Admin" ) );
            }

            var claimsIdentity = new ClaimsIdentity( claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync( CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity) );

            return RedirectToPage( "/Success" );
        }

        public void OnGet() { }
    }
}
