using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.DeliveryCompaniesData
{
    public class AddDeliveryCompanies
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Region { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string? Mobile { get; set; }
        [Required]
        public double Late { get; set; }
        [Required]
        public double Long { get; set; }

        public DeliveryCompanies ToDeliveryCompanies()
        {
            return new DeliveryCompanies
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Balance = 0,
                Late = Late,
                Long = Long,
                Phone = Phone,
                Mobile = Mobile,
                Region = new Region { Id = Region, Enabled = true, Name = "" },
                Enabled = true,
            };
        }
    }
}
