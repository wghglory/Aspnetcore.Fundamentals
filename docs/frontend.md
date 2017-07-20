# Frontend tools and framework

We want to add jquery, jquery-validation, jquery-validation-unobtrusive, bootstrap to project. One way is to use `npm`. Our project should be able to read `node_modules` as static files. To do this, we can create a middleware -- `UseNodeModules` by extending `IApplicationBuilder`

## Create Middleware/ApplicationBuilderExtensions.cs

> Pay attention to the namespace since we're writing an extension method

```csharp
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            var options = new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(root, "node_modules"))
            };

            app.UseStaticFiles(options);

            return app;
        }
    }
}
```

Then we can inject this middleware in Startup.cs

```diff
public void Configure(IApplicationBuilder app ...)
{
    ...

    app.UseFileServer(); //Microsoft.AspNetCore.StaticFiles

+    app.UseNodeModules(env.ContentRootPath); // middleware to read node_modules as static files

    app.UseIdentity();

    // app.UseMvcWithDefaultRoute();
    app.UseMvc(ConfigureRoutes);
}
```

After this we can add any library to views.

###### _Layout.cshtml

```html
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width"/>
    <title>@ViewBag.Title</title>
    <link href="/node_modules/bootstrap/dist/css/bootstrap.css" rel="stylesheet"/>
</head>
<body>
<nav class="navbar navbar-default">
    <div class="container-fluid">
        <div class="navbar-header">
            <a class="navbar-brand" href="/">Food</a>
        </div>
        <div class="navbar-right">
            @await Component.InvokeAsync("LoginLogout")
        </div>
    </div>
</nav>

<div class="container">
    @RenderBody()
    <footer>
        @RenderSection("footer", required: false)
        @await Component.InvokeAsync("Greeting")
    </footer>
</div>
<script src="/node_modules/jquery/dist/jquery.js"></script>
<script src="/node_modules/jquery-validation/dist/jquery.validate.js"></script>
<script src="/node_modules/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"></script>
</body>
</html>
```

It's very convenient to add bootstrap class on the tag helpers

```html
<a class="btn btn-primary" asp-action="Create">Create</a>
```