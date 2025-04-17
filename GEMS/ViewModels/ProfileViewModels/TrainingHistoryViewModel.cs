namespace GEMS.ViewModels.ProfileViewModels
{
    public class TrainingHistoryViewModel
    {
        public string Username { get; set; } = null!;
        public List<UserExerciseWithName> Exercises { get; set; } = null!;
    }

    public class UserExerciseWithName
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; } = null!;
        public int TotalRepsPlayed { get; set; }
        public int TotalMistakesMade { get; set; }
        public DateTime DateTime { get; set; }
    }
}
