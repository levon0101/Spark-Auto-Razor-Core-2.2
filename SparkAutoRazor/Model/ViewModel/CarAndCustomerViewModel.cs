using System.Collections.Generic;

namespace SparkAutoRazor.Model.ViewModel
{
    public class CarAndCustomerViewModel
    { 
        public ApplicationUser UserObj { get; set; }

        public IEnumerable<Car> Cars { get; set; }
    }
}
