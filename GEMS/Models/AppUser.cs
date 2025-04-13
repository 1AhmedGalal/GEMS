using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GEMS.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required.")]
        [MinLength(5)]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required.")]
        [MinLength(5)]
        [MaxLength(20)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Birth Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1/1/1950", nameof(DateTime.Now), ErrorMessage = "Birth Date must be between 1/1/1950 and today.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(30, 150, ErrorMessage = "Weight must be between 30 and 150 kg.")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(100, 210, ErrorMessage = "Height must be between 100 and 210 cm.")]
        public float Height { get; set; } 
    }
}