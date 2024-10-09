using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GiftsStore.DataModels.GiftData
{
    public class AddGift
    {
        [Required]
        public Guid Store { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? MiniDescription { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
    
        public List<IFormFile>? Files { get; set; }

        public Gift ToGift()
        {
            return new Gift
            {
                Id = Guid.NewGuid(),
                MiniDescription = MiniDescription,
                Name = Name,
                Description = Description,
                Price = Price,
                NumRate = 0,
                Rate = 0,
                Store = new Store { Id = Store },
                Enabled = true
            };
        }
    }
}
