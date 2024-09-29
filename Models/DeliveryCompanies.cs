using GiftsStore.DataModels.DeliveryCompaniesData;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class DeliveryCompanies
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public Region? Region { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string? Mobile { get; set; }
        [Required]
        public double Late { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public bool Enabled { get; set; }
        public ViewDeliveryCompanies ToViewDeliveryCompanies()
        {
            return new ViewDeliveryCompanies 
            { 
                Id = Id, 
                Name = Name,
                Balance = Balance,
                Phone = Phone,
                Mobile = Mobile,
                Late = Late,
                Long = Long,
                Region = Region!.Name,  
                Enabled = Enabled,
            };
        }
    }
}
