using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class Region
    {
        [Key]        
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string? Name { get; set; }
        [Required]
        public bool Enabled { get; set; }
    }
}
