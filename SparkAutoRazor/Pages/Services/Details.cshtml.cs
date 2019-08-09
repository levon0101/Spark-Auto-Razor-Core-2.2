using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;

namespace SparkAutoRazor.Pages.Services
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DetailsModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceHeader ServiceHeader { get; set; }

        public List<ServiceDetails> ServiceDetailses { get; set; }
        public void OnGet(int serviceId)
        {
            ServiceHeader = _dbContext.ServiceHeaders
                .Include(c => c.Car)
                .Include(c => c.Car.ApplicationUser)
                .FirstOrDefault(s => s.Id == serviceId);

            ServiceDetailses = _dbContext.ServiceDetailses.Where(s => s.ServiceHeaderId == serviceId).ToList();

        }
    }
}