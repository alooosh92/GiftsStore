using GiftsStore.DataModels.OrderItem;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class OrderItems
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Gift? Gift { get; set; }
        [Required]
        public Order? Order { get; set; }
        [Required]
        public int Number { get; set; }

        public ViewOrderItem ToViewOrderItem()
        {
            return new ViewOrderItem
            {
                GiftName = Gift!.Name,
                Id = Id,
                NumRate = Gift.NumRate,
                Price = Gift.Price,
                Rate = Gift.Rate,
                Url = "",
                Number = Number,
            };
        }
    }
}
