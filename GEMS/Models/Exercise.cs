using System.ComponentModel.DataAnnotations;

namespace GEMS.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(5)]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [MinLength(10)]
        [MaxLength(200)]
        public string? Description { get; set; } = null!;

        public bool isWeighted { get; set; } = false;


    }
}
