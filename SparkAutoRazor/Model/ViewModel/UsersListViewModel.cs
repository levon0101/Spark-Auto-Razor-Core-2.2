using System.Collections.Generic;

namespace SparkAutoRazor.Model.ViewModel
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> ApplicationUserList { get; set; }

        public PagingInfo PagingInfo { get; set; }

    }
}
