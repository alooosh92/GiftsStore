using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.PaymentsDeliveryCompaniesData
{
    public class ViewPaymentsDeliveryCompanies
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? DeliveryCompaniesName { get; set; }
        [Required]
        public Guid DeliveryCompaniesId { get; set; }
        [Required]
        public string? Note { get; set; }
    }
}
