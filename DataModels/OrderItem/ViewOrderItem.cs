using GiftsStore.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.OrderItem
{
    public class ViewOrderItem
    {
        public Guid Id { get; set; }
        public string? GiftName { get; set; }
        public double Rate { get; set; }
        public double NumRate { get; set; }
        public double Price { get; set; }
        public int Number { get; set; } 
        public string? Url {  get; set; } 
       
    }
}
