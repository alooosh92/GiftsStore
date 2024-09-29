using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using GiftsStore.DataModels.ImageData;

namespace GiftsStore.DataModels.StoreData
{
    public class ViewStore
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Region { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public double NumRate { get; set; }
        [Required]
        public double Late { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        [Phone]
        public string? Phone { get; set; }
        [Required]
        [Phone]
        public string? Mobile { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public List<ViewImage>? ListImage { get; set; } 

    }
}
