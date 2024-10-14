using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.OrderData
{
    public class AddOrder
    {
        [Required]
        public double Late { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public string? Person { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Phone]
        public string? Phone { get; set; }
        [Required]
        public string? Notes { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public Guid Store { get; set; }
        public Order ToOrder()
        {
            return new Order
            {
                Address = Address,
                CreateDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Late = Late,
                DeliveryDate = null,
                Long = Long,
                Notes = Notes,
                ToName = Name,
                ToPhone = Phone,
                DeliveryCompanies = null,
                Person = new Person { Id = Person! },
                OrderStatus = "NewCreate",
                Store = new Store { Id = Store },
                ApprovalDate = null,
                WaitingForDeliveryDate = null,
                VerificationDate = null,
                ReadyDate = null
            };
        }
    }
}
