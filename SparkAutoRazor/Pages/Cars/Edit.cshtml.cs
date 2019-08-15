using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;

namespace SparkAutoRazor.Pages.Cars
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public EditModel(ApplicationDbContext dbContext)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var carFromDb = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (carFromDb == null)
            {
                return NotFound();
            }

            carFromDb.VIN = Car.VIN;
            carFromDb.Make = Car.Make;
            carFromDb.Model = Car.Model;
            carFromDb.Miles = Car.Miles;
            carFromDb.Style = Car.Style;
            carFromDb.Color = Car.Color;

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}