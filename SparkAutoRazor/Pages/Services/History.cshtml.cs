using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;

namespace SparkAutoRazor.Pages.Services
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public HistoryModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty] public List<ServiceHeader> ServiceHeaders { get; set; }

        public string UserId { get; set; }
        public async Task<IActionResult> OnGet(int carId)
        {

            ServiceHeaders = await _dbContext.ServiceHeaders
                .Include(s => s.Car)
                .Include(s=>s.Car.ApplicationUser)
                .Where(c=>c.CarId == carId)
                .ToListAsync();
            UserId = (await _dbContext.Cars.Where(u => u.Id == carId).ToListAsync()).FirstOrDefault().UserId;


            return Page(); 
        }
    }
}