using Microsoft.AspNetCore.Mvc;

namespace GEMS.Controllers
{
    public class GuideController : Controller
    {
        public IActionResult Play()
        {
            return View();
        }
    }
}
