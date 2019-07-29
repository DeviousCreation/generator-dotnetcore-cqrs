using Microsoft.AspNetCore.Mvc;

namespace DeviousCreation.CqrsStarterTemplate.Web.Features.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}