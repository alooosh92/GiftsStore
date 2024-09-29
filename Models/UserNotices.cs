using GiftsStore.DataModels.UserNotices;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class UserNotices
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public bool IsRead {  get; set; }
        [Required]
        public Person? Person { get; set; }

        public ViewUserNotices ToViewUserNotices()
        {
            return new() { 
                CreateDate = CreateDate,
                Message = Message,
                Title = Title,
                IsRead = IsRead,
            };
        }
    }
}
