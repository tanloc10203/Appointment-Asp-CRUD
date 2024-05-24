using Microsoft.AspNetCore.Mvc;

namespace Appointment.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
