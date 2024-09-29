using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.ImageData
{
    public class ViewImage
    {
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? URL { get; set; }
    }
}
