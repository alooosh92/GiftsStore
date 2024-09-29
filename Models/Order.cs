using GiftsStore.DataModels.OrderData;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Person? Person { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public double Late { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public string? ToName {  get; set; }
        [Required]
        [Phone]
        public string? ToPhone { get; set; }
        [Required]
        public string? Notes { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required] 
        public DeliveryCompanies? DeliveryCompanies { get; set; }
        [Required] 
        public DateTime? DeliveryDate { get; set; }
        [Required]
        public string? OrderStatus { get; set; }
        [Required]
        public string? DeliveryStatus { get; set; }
        [Required]
        public Store? Store { get; set; } 

        public ViewOrder ToViewOrder()
        {
            return new ViewOrder
            {
                Id = Id,
                Address = Address,
                CreateDate = CreateDate,
                DeliveryCompanyName = DeliveryCompanies!.Name,
                DeliveryDate = DeliveryDate,
                DeliveryStatus = DeliveryStatus,
                Late = Late,
                Long = Long,
                Notes = Notes,
                OrderStatus = OrderStatus,
                FromName = Person!.FullName,
                FromPhone = Person.PhoneNumber,
                ToName = ToName,
                ToPhone = ToPhone,
                Items = new () { },
                IdStore = Store!.Id,
                Region = Store.Region!.Name,
                StoreName = Store.Name,
            };
        }
    }
}
