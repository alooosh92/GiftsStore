using GiftsStore.DataModels.PaymentsStoreData;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class PaymentsStore
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public Store? Store { get; set; }
        [Required]
        public string? Note { get; set; }

        public ViewPaymentsStore ToViewPaymentsStore()
        {
            return new ViewPaymentsStore
            {
                 Id = Id,
                 Balance = Balance, 
                 DateTime = DateTime,   
                 Type = Type,
                 StoreId = Store!.Id,
                 StoreName = Store.Name,
                 Note = Note,
            };
        }
    }
}
