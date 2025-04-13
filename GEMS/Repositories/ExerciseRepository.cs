using GEMS.Models;

namespace GEMS.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly AppDbContext _dbContext;

        public ExerciseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Exercise item)
        {
            _dbContext.Exercises.Add(item);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var exercise = _dbContext.Exercises.FirstOrDefault(e => e.Id == id);

            if (exercise != null)
            {
                _dbContext.Exercises.Remove(exercise);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Exercise wasn't found");
            }
        }

        public List<Exercise>? GetAll()
        {
            return _dbContext.Exercises.ToList();
        }

        public Exercise? GetById(int id)
        {
            return _dbContext.Exercises.FirstOrDefault(e => e.Id == id);
        }

        public void UpdateById(int id, Exercise item)
        {
            var existingExercise = _dbContext.Exercises.FirstOrDefault(e => e.Id == id);

            if (existingExercise != null)
            {
                existingExercise.Name = item.Name;
                existingExercise.Description = item.Description;
                existingExercise.isWeighted = item.isWeighted;

                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Exercise wasn't found");
            }
        }
    }
}
