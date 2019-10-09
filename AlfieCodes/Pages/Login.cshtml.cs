using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlfieCodes.Pages
{
    using AlfieCodes.Data;

    public class LoginModel : PageModel
    {
        public Users Users { get; set; }


        public void OnGet()
        {

        }
    }
}