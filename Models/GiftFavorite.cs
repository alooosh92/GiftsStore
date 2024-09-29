using GiftsStore.DataModels.FavoriteData;
using GiftsStore.DataModels.GiftFavoriteData;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class GiftFavorite
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Person? Person { get; set; }
        [Required]
        public Gift? Gift { get; set; }

        public ViewGiftFavorite ToViewGiftFavorite()
        {
            return new ViewGiftFavorite{
                Gift = Gift!.Id,
                Id = Id
            };
        }
       
    }
}
