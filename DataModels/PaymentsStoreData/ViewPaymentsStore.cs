using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.PaymentsStoreData
{
    public class ViewPaymentsStore
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
        public Guid StoreId { get; set; }
        [Required]
        public string? StoreName { get; set; }
        [Required]
        public string? Note { get; set; }
    }
}
