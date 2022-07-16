using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SEC44NIPSS.Areas.Admin.Pages.Dashboard
{
    [Authorize(Roles = "mSuperAdmin,Admin,Content")]

    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
