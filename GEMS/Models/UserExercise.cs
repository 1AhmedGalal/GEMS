using System.ComponentModel.DataAnnotations;

namespace GEMS.Models
{
    public class UserExercise
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Id is required.")]
        public string UserId { get; set; } = null!;

        [Required(ErrorMessage = "Exercise Id is required.")]
        public int ExerciseId { get; set; }

        [Range(0, 200)]
        public int TotalRepsPlayed { get; set; } = 0;

        [Range(0, 200)]
        public int TotalMistakesMade { get; set; } = 0;

        [Required(ErrorMessage = "Date is required.")]
        public DateTime DateTime { get; set; }
    }
}
