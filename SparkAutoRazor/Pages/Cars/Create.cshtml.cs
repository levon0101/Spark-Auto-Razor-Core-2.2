using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;

namespace SparkAutoRazor.Pages.Cars
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Car Car { get; set; }


        [TempData] public string StatusMessage { get; set; }

        public IActionResult OnGet(string userId = null)
        {
            Car = new Car();
            if (string.IsNullOrEmpty(userId))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                userId = claim.Value;

            }

            Car.UserId = userId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Cars.Add(Car);
            await _dbContext.SaveChangesAsync();

            StatusMessage = "New Car has been created successfully...";

            return RedirectToPage("Index", new { userId = Car.UserId });
        }
    }
}