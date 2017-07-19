# ViewComponents

ViewComponent is a reusable fragment across the application. In the following example we put a GreetingViewComponent in _Layout.cshtml

> note: We already configure IGreeter service in Startup.cs, so we don't need worry about how to get data.

## Create `ViewComponents/GreetingViewComponent.cs`

```csharp
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
```

## Create `Shared/Components/Greeting/Default.cshtml`

```html
@model string

<div>
    Today's message is : @Model
</div>
```

## Render ViewComponent in Layout

```html
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width"/>
    <title>@ViewBag.Title</title>
</head>
<body>
<div>
    @RenderBody()
</div>
<footer>
    @RenderSection("footer", required: false)
    @await Component.InvokeAsync("Greeting")    @*Every page using Layout should see it*@
</footer>
</body>
</html>
```