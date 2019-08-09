﻿using Microsoft.AspNetCore.Identity;

namespace SparkAutoRazor.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }
    }
}
