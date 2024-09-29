using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.StoreFavoriteData
{
    public class ViewStoreFavorite
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Store { get; set; }
    }
}
