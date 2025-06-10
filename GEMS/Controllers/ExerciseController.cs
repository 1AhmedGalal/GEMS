using GEMS.Models;
using GEMS.Repositories;
using GEMS.ViewModels.ExerciseViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GEMS.Controllers
{

    public class ExerciseController : Controller
    {
        private readonly IUserExerciseRepository _userExerciseRepository;
        private readonly UserManager<AppUser> _userManager;

        public ExerciseController(IUserExerciseRepository userExerciseRepository, UserManager<AppUser> userManager)
        {
            _userExerciseRepository = userExerciseRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SaveExerciseData([FromBody] SaveExerciseDataModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var userExercise = new UserExercise
            {
                UserId = user.Id,
                ExerciseId = model.ExerciseId,
                TotalRepsPlayed = model.TotalRepsPlayed,
                TotalMistakesMade = model.TotalMistakesMade,
                DateTime = DateTime.Now
            };

            _userExerciseRepository.Add(userExercise);
            return Ok(new { message = "Data saved successfully" });
        }
 

        public ActionResult Countdown(int id = 0)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult LiveBicep()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Bicep()
        {
            var trigger = new { trigger = "bicep-rule-based" };
            var json = JsonConvert.SerializeObject(trigger);
            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:5000/start", content);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json"); // ✅ FIXED
            }
        }

        public ActionResult LiveSquat()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Squat()
        {
            var trigger = new { trigger = "squat-rule-based" };
            var json = JsonConvert.SerializeObject(trigger);
            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:5000/start", content);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json"); // ✅ FIXED
            }
        }

        public ActionResult LivePushups()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Pushups()
        {
            var trigger = new { trigger = "pushup-rule-based" };
            var json = JsonConvert.SerializeObject(trigger);
            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:5000/start", content);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json"); // ✅ FIXED
            }
        }

        public ActionResult LivePullups()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Pullups()
        {
            var trigger = new { trigger = "pullup-rule-based" };
            var json = JsonConvert.SerializeObject(trigger);
            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:5000/start", content);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json"); // ✅ FIXED
            }
        }

        public ActionResult LiveJumping()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Jumping()
        {
            var trigger = new { trigger = "jumping-rule-based" };
            var json = JsonConvert.SerializeObject(trigger);
            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:5000/start", content);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json"); // ✅ FIXED
            }
        }

        public ActionResult LiveClassification()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Classification()
        {
            var trigger = new { trigger = "classification" };
            var json = JsonConvert.SerializeObject(trigger);
            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:5000/start", content);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json"); // ✅ FIXED
            }
        }

        public ActionResult LivePlank()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Plank()
        {
            var trigger = new { trigger = "plank-correction" };
            var json = JsonConvert.SerializeObject(trigger);
            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:5000/start", content);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
        }


    }
}
