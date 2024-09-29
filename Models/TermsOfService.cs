using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class TermsOfService
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
