using GiftsStore.DataModels.OrderItem;
using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.OrderData
{
    public class ViewOrder
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? VerificationDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ReadyDate { get; set; }
        public DateTime? WaitingForDeliveryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double Late { get; set; }
        public double Long { get; set; }      
        public string? Notes { get; set; }
        public string? Address { get; set; }        
        public string? OrderStatus { get; set; }
        public List<ViewOrderItem>? Items { get; set; }
        public string? FromName { get; set; }
        public string? FromPhone { get; set; }
        public string? ToName { get; set; }
        public string? ToPhone { get; set; }
        public string? DeliveryCompanyName { get; set; }
        public Guid IdStore { get; set; }
        public string? StoreName { get; set; }
        public string? Url {  get; set; }
        public string? Region { get; set; }
    }
}
