using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.OfferData
{
    public class AddOffer
    {
        [Required]
        public Guid Gift { get; set; }
        [Required]
        public int Offer { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public Offers ToOffer()
        {
            return new Offers
            {
                EndDate = EndDate,
                Id = Guid.NewGuid(),
                Offer = Offer,
                StartDate = StartDate,
                Gift = new Gift { Id = Gift }
            };
        }
    }

}
