using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DeviousCreation.CqrsStarterTemplate.Web.Features.Error
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            this.Response.StatusCode = 200;
            return this.View(new ErrorViewModel(Activity.Current?.Id ?? this.HttpContext.TraceIdentifier));
        }
    }
}