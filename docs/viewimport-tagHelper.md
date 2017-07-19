# ViewImports and TagHelper

ViewImports can avoid multiple `using` in Views.

##### Create Views/_ViewImports.cshtml

```csharp
@using Aspnetcore.Fundamentals.Models
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

> Note: Must @addTagHelper so we will get intellisence.

##### Create Views/Home/Edit.cshtml

```html
@model Restaurant

@{
    ViewBag.Title = $"Edit {Model.Name}";
}

<div>
    <h1>Editing: @Model.Name</h1>
    <div>
        <form method="post" asp-antiforgery="true">
            <div>
                <label asp-for="Name"></label>
                <input asp-for="Name"/>
                <span asp-validation-for="Name"></span>
            </div>
            <div>
                <label asp-for="Cuisine"></label>
                <select asp-for="Cuisine" asp-items="@Html.GetEnumSelectList(typeof(CuisineType))"></select>
                <span asp-validation-for="Cuisine"></span>
            </div>
            <div>
                <input type="submit" value="save"/>
            </div>
        </form>
    </div>
</div>
```

##### Views/Home/_Summary.cshtml

```html
@model Restaurant

<section>
    <h3>@Model.Name</h3>
    <div>
        Cuisine: @Model.Cuisine
    </div>
    <div>
        <a asp-action="Details" asp-route-id="@Model.Id">Detail</a>
        <a asp-action="Edit" asp-route-id="@Model.Id">Edit</a>
    </div>
</section>
```