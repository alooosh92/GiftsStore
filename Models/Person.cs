using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace GiftsStore.Models
{
    public class Person:IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        // chenge rang to pin code
        public int PinCode {  get; set; }
        public DateTime CreatePinCode { get; set; }
    }
}
