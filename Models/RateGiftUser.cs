using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class RateGiftUser
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Person? Person { get; set; }
        [Required]
        public Gift? Gift { get; set; }
        [Required]
        public double Rate { get; set; }
    }
}
