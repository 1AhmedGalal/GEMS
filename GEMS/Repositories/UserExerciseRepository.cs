using GEMS.Models;
using Microsoft.EntityFrameworkCore;

namespace GEMS.Repositories
{
    public class UserExerciseRepository : IUserExerciseRepository
    {
        private readonly AppDbContext _dbContext;

        public UserExerciseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(UserExercise item)
        {
            _dbContext.UserExercises.Add(item);
            _dbContext.SaveChanges();
        }

        public UserExercise? GetValue(int id)
        {
            return _dbContext.UserExercises
                .FirstOrDefault(ue => ue.Id == id);
        }

        public List<UserExercise>? GetAllByUserId(string id)
        {
            return _dbContext.UserExercises
                .Where(ue => ue.UserId == id)
                .ToList();
        }

        public void DeleteById(int id)
        {
            var userExercise = _dbContext.UserExercises.FirstOrDefault(ue => ue.Id == id);

            if (userExercise != null)
            {
                _dbContext.UserExercises.Remove(userExercise);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("UserExercise wasn't found");
            }
        }

        public void DeleteByUserId(string id)
        {
            var userExercises = _dbContext.UserExercises
                .Where(ue => ue.UserId == id)
                .ToList();

            if (userExercises.Any())
            {
                _dbContext.UserExercises.RemoveRange(userExercises);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("No UserExercises found for this user");
            }
        }

        public void UpdateById(int id, UserExercise item)
        {
            var existingUserExercise = _dbContext.UserExercises
                .FirstOrDefault(ue => ue.Id == id);

            if (existingUserExercise != null)
            {
                // Validate the exercise exists if it's being changed
                if (existingUserExercise.ExerciseId != item.ExerciseId)
                {
                    var exerciseExists = _dbContext.Exercises.Any(e => e.Id == item.ExerciseId);
                    if (!exerciseExists)
                    {
                        throw new Exception("Specified exercise doesn't exist");
                    }
                }

                existingUserExercise.ExerciseId = item.ExerciseId;
                existingUserExercise.TotalRepsPlayed = item.TotalRepsPlayed;
                existingUserExercise.TotalMistakesMade = item.TotalMistakesMade;
                existingUserExercise.WeightUsed = item.WeightUsed;

                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("UserExercise wasn't found");
            }
        }
    }
}
