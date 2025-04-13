using GEMS.Models;
using GEMS.Repositories;
using GEMS.ViewModels.ProfileViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GEMS.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserExerciseRepository _userExerciseRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(IUserExerciseRepository userExerciseRepository, IExerciseRepository exerciseRepository, UserManager<AppUser> userManager)
        {
            _userExerciseRepository = userExerciseRepository;
            _exerciseRepository = exerciseRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> TrainingHistory()
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get user's exercises
            var userExercises = _userExerciseRepository.GetAllByUserId(user.Id);
            if (userExercises == null)
            {
                return View(new TrainingHistoryViewModel
                {
                    Username = user.UserName!,
                    Exercises = new List<UserExerciseWithName>()
                });
            }

            // Map to view model with exercise names
            var exercisesWithNames = userExercises.Select(ue => new UserExerciseWithName
            {
                Id = ue.Id,
                ExerciseName = _exerciseRepository.GetById(ue.ExerciseId)?.Name ?? "Unknown Exercise",
                TotalRepsPlayed = ue.TotalRepsPlayed,
                TotalMistakesMade = ue.TotalMistakesMade,
                WeightUsed = ue.WeightUsed,
            }).ToList();

            var viewModel = new TrainingHistoryViewModel
            {
                Username = user.UserName!,
                Exercises = exercisesWithNames
            };

            return View(viewModel);
        }
    }
}
