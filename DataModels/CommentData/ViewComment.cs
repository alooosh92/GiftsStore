using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.CommentData
{
    public class ViewComment
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
