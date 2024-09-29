using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class GiftImages
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Gift? Gift { get; set; }
        [Required]
        public string? URL { get; set; }
        [Required]
        public string? Type { get; set; }
    }
}
