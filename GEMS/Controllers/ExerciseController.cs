using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GEMS.Controllers
{
    public class ExerciseController : Controller
    {
        public ActionResult Countdown(int id = 0)
        {
            ViewBag.Id = id;
            return View();
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
