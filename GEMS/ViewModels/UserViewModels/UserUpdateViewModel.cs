using System.ComponentModel.DataAnnotations;

namespace GEMS.ViewModels.UserViewModels
{
    public class UserUpdateViewModel
    {
        [MinLength(5)]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [MinLength(5)]
        [MaxLength(20)]
        public string LastName { get; set; } = null!;

        [DataType(DataType.Password)]
        [MaxLength(30)]
        [Display(Name = "New Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(30)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmedPassword { get; set; }

        [Range(30, 150)]
        public float Weight { get; set; }

        [Range(100, 210)]
        public float Height { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
    }
}
