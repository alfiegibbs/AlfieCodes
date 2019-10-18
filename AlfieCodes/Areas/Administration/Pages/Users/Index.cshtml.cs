namespace AlfieCodes.Areas.Administration.Pages.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class IndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public IReadOnlyCollection<Users> Users { get; private set; }

        public IndexModel(BlogDbContext blogDbContext )
        {
            _blogDbContext = blogDbContext;
        }

        public void OnGet()
        {
            var userList = _blogDbContext.Users.ToList();
            userList.Reverse();
            Users = userList;
        }
    }
}