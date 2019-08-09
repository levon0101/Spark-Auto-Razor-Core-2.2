using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace SparkAutoRazor.Model
{
    public class ServiceShoppingCart
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int ServiceTypeId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }

        [ForeignKey(nameof(ServiceTypeId))]
        public virtual ServiceType ServiceType { get; set; }

    }
}
