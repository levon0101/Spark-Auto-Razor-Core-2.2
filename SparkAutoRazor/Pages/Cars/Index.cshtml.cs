using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;
using SparkAutoRazor.Model.ViewModel;

namespace SparkAutoRazor.Pages.Cars
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public CarAndCustomerViewModel CarAndCustomerVM { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet(string userId = null)
        {
            if(string.IsNullOrEmpty(userId))
            {
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                userId = claim.Value;

            }

            CarAndCustomerVM = new CarAndCustomerViewModel
            {
                UserObj = (ApplicationUser)await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId),
                Cars = _dbContext.Cars.Where(c => c.UserId == userId)
            };

            return Page();
        }
    }
}