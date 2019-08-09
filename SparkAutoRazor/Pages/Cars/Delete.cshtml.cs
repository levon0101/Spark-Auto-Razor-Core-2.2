using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;

namespace SparkAutoRazor.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Car Car { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            Car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (Car == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Car = await _dbContext.Cars.FindAsync(id);

            if (Car == null) return RedirectToPage("./Index");

            _dbContext.Cars.Remove(Car);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}