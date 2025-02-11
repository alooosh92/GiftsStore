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
        public string? Name { get; set; }
        [Required]
        [Phone]
        public string? Phone { get; set; }
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
                Long = Long,
                Notes = Notes,
                ToName = Name,
                ToPhone = Phone,
                OrderStatus = "NewCreate",
                Store = new Store { Id = Store },
            };
        }
    }
}
