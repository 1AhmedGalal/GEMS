using GEMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace GEMS.Repositories
{
    public interface IExerciseRepository
    {
        Exercise? GetById(int id);

        List<Exercise>? GetAll();

        void Add(Exercise item);

        void DeleteById(int id);

        void UpdateById(int id, Exercise item);

    }
}
