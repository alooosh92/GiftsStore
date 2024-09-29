using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.UserNotices
{
    public class ViewUserNotices
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public bool IsRead { get; set; }
    }
}
