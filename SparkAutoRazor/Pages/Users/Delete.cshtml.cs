using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;
using SparkAutoRazor.Utility;

namespace SparkAutoRazor.Pages.Users
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty] public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id.Trim().Length == 0)
            {
                return NotFound();
            }

            ApplicationUser = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);

            if (ApplicationUser == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id.Trim().Length == 0)
            {
                return NotFound();
            }

            var appUserDb = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);

            if (appUserDb == null)
            {
                return NotFound();
            }

            _dbContext.ApplicationUsers.Remove(appUserDb);

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}