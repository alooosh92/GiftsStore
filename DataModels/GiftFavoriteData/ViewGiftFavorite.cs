using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.GiftFavoriteData
{
    public class ViewGiftFavorite
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Gift { get; set; }
    }
}
