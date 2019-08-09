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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var appUserDb = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(s => s.Id == ApplicationUser.Id);

            if (appUserDb == null)
            {
                return NotFound();
            }

            appUserDb.Name = ApplicationUser.Name;
            appUserDb.Email = ApplicationUser.Email;
            appUserDb.PhoneNumber = ApplicationUser.PhoneNumber;
            appUserDb.Address = ApplicationUser.Address;
            appUserDb.City = ApplicationUser.City;
            appUserDb.PostalCode = ApplicationUser.PostalCode;


            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}