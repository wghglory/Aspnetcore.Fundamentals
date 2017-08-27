# ViewComponents

ViewComponent is a reusable fragment across the application. In the following example we put a GreetingViewComponent in _Layout.cshtml

> note: We already configure IGreeter service in Startup.cs, so we don't need worry about how to get data.

## ViewComponent vs PartialView

partial在母版页调用，但只能传递母版页的 model 或者不传。如果母版页显示产品列表，partial负责显示库存。库存和产品来自两个类，则解决办法只有创建一个 viewModel，包含这两个类。第二个副效应是使用了 viewmodel 后，post表单也需要传递 viewModel，其实我可能只编辑了 product，即只想用 product 而非 viewmodel 来接受。如果post控制器用 product 接收，表单提交时不能匹配 product。因为模型绑定时属性名字是 `Product_Name` 而不是 `Name`，解决办法是需要在控制器Product前加上 `[Bind="Product"]`。

viewComponents可以单独传model。有自己的业务逻辑。是 PartialView 的升级版。

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
