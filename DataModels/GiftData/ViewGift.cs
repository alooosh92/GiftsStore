using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GiftsStore.DataModels.GiftData
{
    public class ViewGift
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public double NumRate { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public string? StoreName { get; set; }
        [Required]
        public string? Region { get; set; }
        [Required]
        public List<GiftImages>? GiftImages { get; set; }
        [Required]
        public bool Enabled { get; set; }
    }
}
