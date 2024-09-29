using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.FavoriteData
{
    public class AddStoreFavorite
    {
        [Required]
        public Person? Person { get; set; }
        [Required]
        public Guid Store { get; set; }

        public StoreFavorite ToFavorite()
        { 
            return new StoreFavorite 
            {
                Id = Guid.NewGuid(),
                Store = new Store { Id = Store},
                Person = Person,
            };
        }
    }

}
