using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GiftsStore.DataModels.StoreData
{
    public class AddStore
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Region { get; set; }
        [Required]
        public double Late { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        [Phone]
        public string? Phone { get; set; }
        [Required]
        [Phone]
        public string? Mobile { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public List<IFormFile>? Files { get; set; }

        public Store ToStore()
        {
            return new Store
            {
                Created = DateTime.Now,
                Name = Name,
                Description = Description,
                Phone = Phone,
                Id = Guid.NewGuid(),
                Late = Late,
                Long = Long,
                Mobile = Mobile,
                Type = Type,
                NumRate = 0,
                Rate = 0,
                Balance = 0,
                Enabled = true,
                Region = new Region { Id = Region, Enabled = true, Name = "" },
            };
        }
    }
}
