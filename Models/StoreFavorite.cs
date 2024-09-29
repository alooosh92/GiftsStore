using GiftsStore.DataModels.FavoriteData;
using GiftsStore.DataModels.GiftFavoriteData;
using GiftsStore.DataModels.StoreFavoriteData;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class StoreFavorite
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Person? Person { get; set; }
        [Required]
        public Store? Store { get; set; }

        public ViewStoreFavorite ToViewGiftFavorite()
        {
            return new ViewStoreFavorite
            {
                Store = Store!.Id,
                Id = Id
            };
        }
    }
}
