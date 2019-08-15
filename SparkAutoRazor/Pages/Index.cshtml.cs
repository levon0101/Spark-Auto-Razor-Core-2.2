using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAutoRazor.Utility;

namespace SparkAutoRazor.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return RedirectToPage("Account/Login", new { area = "Identity" });
            }

            if (User.IsInRole(StaticDetails.AdminEndUser))
            {
                return RedirectToPage("/Users/Index");
            }

            return RedirectToPage("/Cars/Index");

        }
    }
}
