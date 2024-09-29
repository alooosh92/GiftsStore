using GiftsStore.DataModels.PaymentsDeliveryCompaniesData;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class PaymentsDeliveryCompanies
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
        public DeliveryCompanies? DeliveryCompanies { get; set; }
        [Required]
        public string? Note { get; set; }

        public ViewPaymentsDeliveryCompanies ToViewPaymentsDeliveryCompanie()
        {
            return new ViewPaymentsDeliveryCompanies
            {
                Id = Id,
                Balance = Balance,
                DateTime = DateTime,
                Type = Type,
                DeliveryCompaniesId = DeliveryCompanies!.Id,
                DeliveryCompaniesName = DeliveryCompanies.Name,
                Note = Note,
            };
        }
    }
}
