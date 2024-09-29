using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.PaymentsStoreData
{
    public class AddPaymentsStore
    {
        [Required]
        public double Balance { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public Guid Store { get; set; }
        [Required]
        public string? Note { get; set; }

        public PaymentsStore ToPaymentsStore()
        {
            return new PaymentsStore 
            { 
                Balance = Balance,
                Type = Type,
                DateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Store = new Store { Id = Store}, 
                Note = Note,
            };
        }
    }
}
