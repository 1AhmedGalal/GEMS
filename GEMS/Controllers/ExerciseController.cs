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

        private async Task<string> SendTriggerToPythonAsync(string triggerValue)
        {
            var trigger = new { trigger = triggerValue };
            var json = JsonConvert.SerializeObject(trigger);

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:5000/start", content);
                return await response.Content.ReadAsStringAsync();
            }
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

        

        public ActionResult LiveSquat()
        {
            return View();
        }

        

        public ActionResult LivePushups()
        {
            return View();
        }


        public ActionResult LivePullups()
        {
            return View();
        }

       

        public ActionResult LiveLegRaises()
        {
            return View();
        }

        

        public ActionResult LiveFrontRaises()
        {
            return View();
        }

        

        public ActionResult LiveBenchPress()
        {
            return View();
        }

        

        public ActionResult LiveJumping()
        {
            return View();
        }

        

        public ActionResult LiveClassification()
        {
            return View();
        }

        

        public ActionResult LivePlank()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Bicep()
        {
            var result = await SendTriggerToPythonAsync("bicep-rule-based");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> Squat()
        {
            var result = await SendTriggerToPythonAsync("squat-rule-based");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> Pushups()
        {
            var result = await SendTriggerToPythonAsync("pushup-rule-based");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> Pullups()
        {
            var result = await SendTriggerToPythonAsync("pullup-rule-based");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> LegRaises()
        {
            var result = await SendTriggerToPythonAsync("leg_raise-rule-based");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> FrontRaises()
        {
            var result = await SendTriggerToPythonAsync("front_raise-rule-based");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> BenchPress()
        {
            var result = await SendTriggerToPythonAsync("bench-rule-based");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> Jumping()
        {
            var result = await SendTriggerToPythonAsync("jumping-rule-based");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> Classification()
        {
            var result = await SendTriggerToPythonAsync("classification");
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> Plank()
        {
            var result = await SendTriggerToPythonAsync("plank-correction");
            return Content(result, "application/json");
        }



    }
}
