using System.ComponentModel.DataAnnotations;

namespace GEMS.ViewModels.UserViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30)]
        public string? Email { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string? Username { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmedPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(30, 150)]
        public float Weight { get; set; }

        [Required]
        [Range(100, 210)]
        public float Height { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}