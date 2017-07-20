using Microsoft.AspNetCore.Mvc;

namespace Aspnetcore.Fundamentals.ViewComponents
{
    public class LoginLogoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}