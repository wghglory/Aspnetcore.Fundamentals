using System.Threading.Tasks;
using Aspnetcore.Fundamentals.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aspnetcore.Fundamentals.ViewComponents
{
    public class GreetingViewComponent : ViewComponent
    {
        private readonly IGreeter _greeter;

        public GreetingViewComponent(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            var model = _greeter.GetGreeting();
            return Task.FromResult<IViewComponentResult>(View("Default", model));
        }
    }
}