﻿using System.Collections.Generic;

namespace SparkAutoRazor.Model.ViewModel
{
    public class CarServiceViewModel
    {
        public Car Car { get; set; }

        public ServiceHeader ServiceHeader { get; set; }

        public ServiceDetails ServiceDetails { get; set; }

        public List<ServiceType> ServiceTypesList { get; set; }

        public List<ServiceShoppingCart> ServiceShoppingCart { get; set; } 

    }
}
