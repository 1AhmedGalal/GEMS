using GEMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace GEMS.Repositories
{
    public interface IUserExerciseRepository
    {
        UserExercise? GetValue(int id);

        List<UserExercise>? GetAllByUserId(string id);

        void Add(UserExercise item);

        void DeleteById(int id);

        void DeleteByUserId(string id);

        void UpdateById(int id, UserExercise item);
    }
}
