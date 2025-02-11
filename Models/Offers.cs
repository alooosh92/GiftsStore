using GiftsStore.DataModels.OfferData;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class Offers
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Gift? Gift { get; set; }
        [Required]
        public int Offer { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public ViewOffer ToViewOffer()
        {
            var VGift = Gift?.toViewGift();
            return new ViewOffer
            {
                Id = Id,
                EndDate = EndDate,
                Offer = Offer,
                StartDate = StartDate,
                GiftId = VGift!.Id,
                GiftImages = VGift.GiftImages,
                MiniDescription = VGift.MiniDescription,
                Name = VGift.Name,
                NumRate = VGift.NumRate,
                Price = VGift.Price,
                Rate = VGift.Rate,
                Region = VGift.Region,
                StoreId = VGift.StoreId,
                StoreName = VGift.StoreName
                
            };
        }
    }
}
