using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.DeliveryCompaniesData
{
    public class ViewDeliveryCompanies
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Region { get; set; }
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
    }
}
