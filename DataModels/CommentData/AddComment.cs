using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.CommentData
{
    public class AddComment
    {
        [Required]
        public string? Text { get; set; }
        [Required]
        public Guid StoreId { get; set; }

        public Comment ToComment(Person person)
        {
            return new Comment
            {
                Person = person,
                Text = Text,
                CreateDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Store = new Store { Id = StoreId },
                Report = false
            };
        }
    }
}
