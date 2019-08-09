using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;
using SparkAutoRazor.Utility;

namespace SparkAutoRazor.Pages.ServiceTypes
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private readonly SparkAutoRazor.Data.ApplicationDbContext _dbContext;

        public DeleteModel(SparkAutoRazor.Data.ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        { 
            ServiceType = await _dbContext.ServiceTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (ServiceType == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
             
            ServiceType = await _dbContext.ServiceTypes.FindAsync(id);

            if (ServiceType == null) return RedirectToPage("./Index");


            _dbContext.ServiceTypes.Remove(ServiceType);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
