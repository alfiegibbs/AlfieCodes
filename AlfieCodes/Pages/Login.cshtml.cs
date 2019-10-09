using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlfieCodes.Pages
{
    using AlfieCodes.Data;
    using AlfieCodes.Models;
    using BCrypt.Net;

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

        public RedirectResult OnPost()
        {
            if ( VerifyInformation() == false )
            {
                return Redirect( "/Fail" );
            }

            return Redirect( "/Success" );
        }

        public void OnGet() { }
    }
}
