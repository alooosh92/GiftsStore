using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.OrderItem
{
    public class AddItem
    {
        [Required]
        public Guid GiftId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public int Number { get; set; }
    }
}
