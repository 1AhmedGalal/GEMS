using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GEMS.Controllers
{
    public class ExerciseController : Controller
    {
        public ActionResult LiveSquat()
        {
            return View();
        }
       

        // Call this once to send the trigger to Flask
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

    }
}
