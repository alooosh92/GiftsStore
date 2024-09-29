using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.FavoriteData
{
    public class AddGiftFavorite
    {
        [Required]
        public Person? Person { get; set; }
        [Required]
        public Guid Gift { get; set; }

        public GiftFavorite ToFavorite()
        { 
            return new GiftFavorite 
            {
                Id = Guid.NewGuid(),
                Gift = new Gift { Id = Gift},
                Person =  Person ,
            };
        }
    }

}
