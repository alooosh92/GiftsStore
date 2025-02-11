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
        public DateTime? VerificationDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ReadyDate { get; set; }
        public DateTime? WaitingForDeliveryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        [Required]
        public double Late { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public string? ToName {  get; set; }
        [Required]
        [Phone]
        public string? ToPhone { get; set; }
        public string? Notes { get; set; }
        [Required]
        public string? Address { get; set; }
        public DeliveryCompanies? DeliveryCompanies { get; set; }       
        [Required]
        public string? OrderStatus { get; set; }
        public Store? Store { get; set; } 
        public double Offers { get; set; }
        public ViewOrder ToViewOrder()
        {
            return new ViewOrder
            {
                Id = Id,
                Address = Address,
                CreateDate = CreateDate,
                DeliveryCompanyName = DeliveryCompanies?.Name,
                DeliveryDate = DeliveryDate,
                Late = Late,
                Long = Long,
                Notes = Notes,
                OrderStatus = OrderStatus,
                FromName = Person?.FullName,
                FromPhone = Person?.PhoneNumber,
                ToName = ToName,
                ToPhone = ToPhone,
                Items = new () { },
                IdStore = Store?.Id,
                Region = Store?.Region?.Name,
                StoreName = Store?.Name,
                ApprovalDate = ApprovalDate,
                ReadyDate = ReadyDate,
                VerificationDate = VerificationDate,                
                WaitingForDeliveryDate = WaitingForDeliveryDate,
                Offers = Offers,
            };
        }
    }
}
