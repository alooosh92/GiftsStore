using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.PaymentsDeliveryCompaniesData
{
    public class AddPaymentsDeliveryCompanies
    {
        [Required]
        public double Balance { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public Guid DeliveryCompanies { get; set; }
        [Required]
        public string? Note { get; set; }

        public PaymentsDeliveryCompanies ToPaymentsDeliveryCompanies()
        {
            return new PaymentsDeliveryCompanies
            {
                Balance = Balance,
                Type = Type,
                DateTime = DateTime.Now,
                DeliveryCompanies = new DeliveryCompanies { Id = DeliveryCompanies },
                Id = Guid.NewGuid(),
                Note = Note
            };
        }
    }
}
