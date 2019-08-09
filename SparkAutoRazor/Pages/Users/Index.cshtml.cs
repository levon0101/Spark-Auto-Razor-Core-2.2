using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAutoRazor.Data;
using SparkAutoRazor.Model;
using SparkAutoRazor.Model.ViewModel;
using SparkAutoRazor.Utility;

namespace SparkAutoRazor.Pages.Users
{
    [Authorize(Roles = StaticDetails.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public UsersListViewModel UsersListViewModel { get; set; }

        public async Task<IActionResult> OnGet(int productPage = 1, string searchEmail = null, string searchName = null, string searchPhone = null)
        {
            UsersListViewModel = new UsersListViewModel
            {
                ApplicationUserList = await _dbContext.ApplicationUsers.ToListAsync()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Users?productPage=:");
            param.Append("&searchName");
            if (!string.IsNullOrEmpty(searchName))
            {
                param.Append(searchName);
            }

            param.Append("&searchEmail");
            if (!string.IsNullOrEmpty(searchEmail))
            {
                param.Append(searchEmail);
            }

            param.Append("&searchPhone");
            if (!string.IsNullOrEmpty(searchPhone))
            {
                param.Append(searchPhone);
            }


            if (!string.IsNullOrEmpty(searchEmail) || !string.IsNullOrEmpty(searchName) || !string.IsNullOrEmpty(searchPhone))
            {
                UsersListViewModel.ApplicationUserList = await _dbContext.ApplicationUsers
                    .Where(u => u.Email
                                    .ToLower()
                                    .Contains(searchEmail !=null ? searchEmail.ToLower() : "") && 
                                u.Name
                                    .ToLower()
                                    .Contains(searchName != null ? searchName.ToLower() : "") &&
                                u.PhoneNumber
                                    .ToLower()
                                    .Contains(searchPhone != null ? searchPhone.ToLower() : "")
                                )
                    .ToListAsync();
            }


            var count = UsersListViewModel.ApplicationUserList.Count;

            UsersListViewModel.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = StaticDetails.PaginationUsersPageSize,
                TotalItems = count,
                UrlParam = param.ToString()
            };

            UsersListViewModel.ApplicationUserList = UsersListViewModel.ApplicationUserList
                .OrderBy(p => p.Email)
                .Skip((productPage - 1) * StaticDetails.PaginationUsersPageSize)
                .Take(StaticDetails.PaginationUsersPageSize)
                .ToList();

            return Page();
        }
    }
}