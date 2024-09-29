using GiftsStore.DataModels.StoreData;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.Models
{
    public class Store
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public Region? Region { get; set; }
        [Required]
        public DateTime? Created { get; set; }
        [Required]
        [DefaultValue(0)]
        public double Rate { get; set; }
        [Required]
        [DefaultValue(0)]
        public double NumRate {  get; set; }
        [Required]
        public double Late {  get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        [Phone]
        public string? Phone { get; set; }
        [Required]
        [Phone]
        public string? Mobile { get; set; }
        [Required]
        public bool Enabled { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public string? Type { get; set; }

        public ViewStore ToViewStore()
        {
            return new ViewStore
            {
                Id = Id,
                Region = Region!.Name,
                Name = Name,
                Rate = Rate,
                NumRate = NumRate,
                Description = Description,
                Late = Late,
                Long = Long,
                Mobile = Mobile,
                Phone = Phone,
                Balance = Balance,
                Type = Type,
                ListImage = new()
            };
        }
    }
}
