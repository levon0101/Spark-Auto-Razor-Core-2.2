﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SparkAutoRazor.Model
{
    public class ServiceHeader
    {
        public int Id { get; set; }

        public double Miles { get; set; }

        [Required] public double TotalPrice { get; set; }

        public string Details { get; set; }


        [Required] 
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
        public DateTime DateAdded { get; set; }

        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))] public Car Car { get; set; } 
    }
}
