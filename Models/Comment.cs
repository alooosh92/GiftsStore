using GiftsStore.DataModels.CommentData;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public Person? Person { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public Store? Store { get; set; }
        public bool Report { get; set; }

        public ViewComment ToViewComment()
        {
            return new ViewComment
            {
                CreateDate = CreateDate,
                FullName = Person!.FullName,
                Id = Id,
                Phone = Person.PhoneNumber,
                Text = Text
            };
        }
    }
}
