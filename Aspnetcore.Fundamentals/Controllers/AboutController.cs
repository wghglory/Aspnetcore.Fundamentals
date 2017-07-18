using Microsoft.AspNetCore.Mvc;

namespace Aspnetcore.Fundamentals.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController : Controller
    {
        public string Phone()
        {
            return "1+555-555-5555";
        }

        public string Address()
        {
            return "USA";
        }
    }
}