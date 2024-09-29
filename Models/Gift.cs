using GiftsStore.DataModels.GiftData;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class Gift
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Store? Store { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
      [Required]
        [DefaultValue(0)]
        public double Rate { get; set; }
        [Required]
        [DefaultValue(0)]
        public double NumRate { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool Enabled { get; set; }

        public ViewGift toViewGift()
        {
            return new ViewGift
            {
                 Id = Id,
                 Description = Description,
                 Rate = Rate,
                 NumRate = NumRate,
                 Price = Price,
                 Name = Name,
                 Region = Store!.Region!.Name,
                 StoreId = Store.Id,
                 StoreName = Store.Name,
                 GiftImages = new() {},
                 Enabled = Enabled,
            };
        }
    }
}
