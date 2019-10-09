using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlfieCodes.Pages
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;

    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.SignOutAsync(
                                     CookieAuthenticationDefaults.AuthenticationScheme );
            RedirectToPage( "/Index" );
        }
    }
}