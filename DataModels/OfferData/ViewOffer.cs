using GiftsStore.DataModels.ImageData;
using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.OfferData
{
    public class ViewOffer
    {
        [Key]
        public Guid Id { get; set; }        
        [Required]
        public int Offer { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Key]
        public Guid GiftId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? MiniDescription { get; set; }
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
        public List<ViewImage>? GiftImages { get; set; }
    }
}
