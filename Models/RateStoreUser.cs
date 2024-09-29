using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class RateStoreUser
    {
        [Key] 
        public Guid Id { get; set; }
        [Required]
        public Person? Person { get; set; }
        [Required] 
        public Store? Store { get; set; }
        [Required]
        public double Rate { get; set; }
    }
}
