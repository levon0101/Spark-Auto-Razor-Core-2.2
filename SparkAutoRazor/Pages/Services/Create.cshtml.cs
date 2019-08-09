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
using SparkAutoRazor.Model.ViewModel;
using SparkAutoRazor.Utility;

namespace SparkAutoRazor.Pages.Services
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public CarServiceViewModel CarServiceVM { get; set; }

        public async Task<IActionResult> OnGet(int carId)
        {
            CarServiceVM = new CarServiceViewModel
            {
                Car = await _dbContext.Cars.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == carId),
                ServiceHeader = new ServiceHeader()
            };

            List<string> listServiceTypeInShoppingCart = await _dbContext.ServiceShoppingCarts
                                                            .Include(s => s.ServiceType)
                                                            .Where(c => c.CarId == carId)
                                                            .Select(c => c.ServiceType.Name)
                                                            .ToListAsync();


            IQueryable<ServiceType> lstService = from s in _dbContext.ServiceTypes
                                                 where !listServiceTypeInShoppingCart.Contains(s.Name)
                                                 select s;
            CarServiceVM.ServiceTypesList = await lstService.ToListAsync();

            CarServiceVM.ServiceShoppingCart = await _dbContext.ServiceShoppingCarts
                .Include(s => s.ServiceType)
                .Where(c => c.CarId == carId)
                .ToListAsync();

            CarServiceVM.ServiceHeader.TotalPrice = 0;

            foreach (var item in CarServiceVM.ServiceShoppingCart)
            {
                CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;

            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CarServiceVM.ServiceHeader.DateAdded = DateTime.Now;
            CarServiceVM.ServiceShoppingCart = _dbContext.ServiceShoppingCarts
                .Include(c => c.ServiceType)
                .Where(c => c.CarId == CarServiceVM.Car.Id)
                .ToList();


            foreach (var item in CarServiceVM.ServiceShoppingCart)
            {

                CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;
            }

            CarServiceVM.ServiceHeader.CarId = CarServiceVM.Car.Id;

            _dbContext.ServiceHeaders.Add(CarServiceVM.ServiceHeader);
            await _dbContext.SaveChangesAsync();

            foreach (var detail in CarServiceVM.ServiceShoppingCart)
            {
                var serviceDetails = new ServiceDetails
                {
                    ServiceHeaderId = CarServiceVM.ServiceHeader.Id,
                    ServiceName = detail.ServiceType.Name,
                    ServicePrice = detail.ServiceType.Price,
                    ServiceTypeId = detail.ServiceTypeId
                };
                _dbContext.ServiceDetailses.Add(serviceDetails);
            }
            _dbContext.ServiceShoppingCarts.RemoveRange(CarServiceVM.ServiceShoppingCart);

            await _dbContext.SaveChangesAsync();


            return RedirectToPage("../Cars/Index", new { userId = CarServiceVM.Car.UserId });


        }

        public async Task<IActionResult> OnPostAddToCart()
        {
            ServiceShoppingCart objServiceCart = new ServiceShoppingCart
            {
                CarId = CarServiceVM.Car.Id,
                ServiceTypeId = CarServiceVM.ServiceDetails.ServiceTypeId,
            };

            _dbContext.ServiceShoppingCarts.Add(objServiceCart);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Create", new { carId = CarServiceVM.Car.Id });
        }

        public async Task<IActionResult> OnPostRemoveFromCart(int serviceTypeId)
        {
            ServiceShoppingCart objServiceCartDb =
                await _dbContext.ServiceShoppingCarts.FirstOrDefaultAsync(u => u.ServiceTypeId == serviceTypeId &&
                                                                               u.CarId == CarServiceVM.Car.Id);


            _dbContext.ServiceShoppingCarts.Remove(objServiceCartDb);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Create", new { carId = CarServiceVM.Car.Id });
        }
    }
}