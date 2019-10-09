using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlfieCodes.Pages
{
    using System.Security.Claims;
    using AlfieCodes.Data;
    using AlfieCodes.Models;
    using BCrypt.Net;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;

    public class LoginModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public LoginModel( BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        [ BindProperty ]
        public LoginRequest LoginRequest { get; set; }

        private bool VerifyInformation()
        {
            var user = _blogDbContext.Users.FirstOrDefault( obj => obj.Email == LoginRequest.Email );

            if ( user == null )
            {
                return false;
            }

            bool encryptedPassword = BCrypt.Verify( LoginRequest.Password, user.Password );
            
            return encryptedPassword;
        }

        public async Task<IActionResult> OnPost()
        {
            if ( VerifyInformation() == false )
            {
                return Redirect( "/Fail" );
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, LoginRequest.Email),
            };

            var claimsIdentity = new ClaimsIdentity( claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync( CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity) );

            return RedirectToPage( "/Success" );
        }

        public void OnGet() { }
    }
}
