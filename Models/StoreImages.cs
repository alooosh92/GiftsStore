using GiftsStore.DataModels.ImageData;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class StoreImages
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Store? Store { get; set; }
        [Required]
        public string? Type {  get; set; }
        [Required]
        public string? URL { get; set; }

        public ViewImage ToViewImage()
        {
            return new ViewImage { 
                Type = Type,
                URL = URL,
            };
        }
    }
}
