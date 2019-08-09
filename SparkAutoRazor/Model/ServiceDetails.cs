using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SparkAutoRazor.Model
{
    public class ServiceDetails
    {
        public int Id { get; set; }

        public int ServiceHeaderId { get; set; }

        [ForeignKey(nameof(ServiceHeaderId))]
        public ServiceHeader ServiceHeader { get; set; }

        [Display(Name = "Service")]
        public int ServiceTypeId { get; set; }

        [ForeignKey(nameof(ServiceTypeId))]
        public virtual ServiceType ServiceType { get; set; }

        public double ServicePrice { get; set; }

        public string ServiceName { get; set; }
        

    }
}
