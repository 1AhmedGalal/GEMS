using GEMS.Models;
using GEMS.Repositories;
using GEMS.ViewModels.ProfileViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace GEMS.Controllers
{
    public class PerformanceController : Controller
    {
        private readonly IUserExerciseRepository _userExerciseRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private readonly string _apiKey;

        public PerformanceController(
            IUserExerciseRepository userExerciseRepository,
            IExerciseRepository exerciseRepository,
            UserManager<AppUser> userManager,
            IHttpClientFactory httpClientFactory,
            IMemoryCache cache,
            IConfiguration configuration)
        {
            _userExerciseRepository = userExerciseRepository;
            _exerciseRepository = exerciseRepository;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _apiKey = configuration.GetValue<string>("GroqApiKey")!;
        }

        private string ExerciseBreakdown(List<UserExerciseWithName> exercisesWithNames) => string.Join("\n", exercisesWithNames.Select(e => $"- {e.ExerciseName}: {e.TotalRepsPlayed} reps, {e.TotalMistakesMade} mistakes"));

        private async Task<string> AskGroq(string prompt)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var requestBody = new
            {
                model = "llama3-8b-8192",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var response = await client.PostAsJsonAsync("https://api.groq.com/openai/v1/chat/completions", requestBody);
            if (!response.IsSuccessStatusCode)
                return $"⚠️ Error: {(int)response.StatusCode} - {await response.Content.ReadAsStringAsync()}";

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> TrainingHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var userExercises = _userExerciseRepository.GetAllByUserId(user.Id);
            var exercisesWithNames = userExercises!.Select(ue => new UserExerciseWithName
            {
                Id = ue.Id,
                ExerciseName = _exerciseRepository.GetById(ue.ExerciseId)?.Name ?? "Unknown Exercise",
                TotalRepsPlayed = ue.TotalRepsPlayed,
                TotalMistakesMade = ue.TotalMistakesMade,
                DateTime = ue.DateTime
            }).ToList();

            var viewModel = new TrainingHistoryViewModel
            {
                Username = user.UserName!,
                Exercises = exercisesWithNames
            };

            var breakdown = ExerciseBreakdown(exercisesWithNames);

            var prompt = $@"
Here is a user's workout data per exercise:
{breakdown}

Give 5 bullet points of technical advice.
Each bullet must be 20 words or fewer.
Focus only on the exercises provided above.
Tips should be form-related: what to adjust, correct, or avoid.
No motivational phrases.
No references to personal trainers, paid programs, or services.
Only provide practical technique tips for each named exercise.
Do You Think User Performed good in that exercise? [make that part of each bullet point].
Display name of exercise in beginning of each point.
If User Performed good add a suitable emoji for that and same goes if her performed badly [this is not counted in the 20 words] [like and dislike or simly face and sad face].
Don't use signs like '+' or '*' or '**' even if you are trying to format text.
";

            string cacheKey = $"chatgpt_advice_user_{user.Id}";
            if (!_cache.TryGetValue(cacheKey, out string advice))
            {
                advice = await AskGroq(prompt);
                if (!advice.Contains("⚠️"))
                    _cache.Set(cacheKey, advice, TimeSpan.FromSeconds(10));
            }

            ViewBag.ChatAdvice = advice;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AskQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DetailedAdvice(string userMessage)
        {
            userMessage += $@"\n
Note There is only these exercise:
jumping jacks
bicep curl
squat
push ups
bench press
planks
front raises
leg raises
pull ups
";
            var reply = await AskGroq(userMessage);
            ViewBag.GroqResponse = reply;
            return View();
        }
    }
}
