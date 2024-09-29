using GiftsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftsStore.DataModels.PrivacyAndTerm
{
    public class PrivacyAndTermsAdd
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public PrivacyPolicy ToPrivacyPolicy()
        {
            return new PrivacyPolicy
            {
                Title = Title,
                Description = Description
            };
        }
        public TermsOfService ToTermsOfService()
        {
            return new TermsOfService
            {
                Title = Title,
                Description = Description
            };
        }
    }
    
}
