# Identity

```bash
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore  # PMC (Package Manager Console)
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore. # bash
```

## Create User model using IdentityUser

##### Models/User.cs

```csharp
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aspnetcore.Fundamentals.Models
{
    public class User : IdentityUser
    {
        // ...
    }
}
```

## Change DbContext

```csharp
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aspnetcore.Fundamentals.Models
{
    public class FoodDbContext : IdentityDbContext<User>
    {
        public FoodDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
```

## Configure Identity in Startup.cs

```csharp
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
...

namespace Aspnetcore.Fundamentals
{
    public class Startup
    {       
        public void ConfigureServices(IServiceCollection services)
        {
          	...

            services.AddDbContext<FoodDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FoodDbContext>();
        }

        public void Configure(...)
        {
            ...
              
            app.UseFileServer(); //Microsoft.AspNetCore.StaticFiles

            app.UseIdentity();

            // app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoutes);
        }
    }
}
```

## Create 2 viewmodels

##### LoginViewModel.cs

```csharp
using System.ComponentModel.DataAnnotations;

namespace Aspnetcore.Fundamentals.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
```

##### RegisterViewModel.cs

```csharp
using System.ComponentModel.DataAnnotations;

namespace Aspnetcore.Fundamentals.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
```

## Create Account Controller

```csharp
using System.Threading.Tasks;
using Aspnetcore.Fundamentals.Models;
using Aspnetcore.Fundamentals.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aspnetcore.Fundamentals.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userMangager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userMangager, SignInManager<User> signInManager)
        {
            _userMangager = userMangager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User {UserName = model.Username};

                var createResult = await _userMangager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false); //isPersistent:false
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult =
                    await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Could not login");
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
```

## Register.cshtml

```html
@model RegisterViewModel

@{
    ViewBag.Title = "Register";
}

<h1>Register</h1>

<form method="post" asp-antiforgery="true">

    <div asp-validation-summary="ModelOnly"></div>

    <div>
        <label asp-for="Username"></label>
        <input asp-for="Username" value="wghglory"/>
        <span asp-validation-for="Username"></span>
    </div>

    <div>
        <label asp-for="Password"></label>
        <input asp-for="Password" value="Wgh123123!"/>
        <span asp-validation-for="Password"></span>
    </div>

    <div>
        <label asp-for="ConfirmPassword"></label>
        <input asp-for="ConfirmPassword" value="Wgh123123!"/>
        <span asp-validation-for="ConfirmPassword"></span>
    </div>

    <div>
        <input type="submit" value="Register"/>
    </div>

</form>
```

Login.csthml

```html
@model LoginViewModel
@{
    ViewBag.Title = "Login";
}

<h2>Login</h2>

<form method="post" asp-antiforgery="true">
    <div asp-validation-summary="ModelOnly"></div>
    <div>
        <label asp-for="Username"></label>
        <input asp-for="Username" value="wghglory"/>
        <span asp-validation-for="Username"></span>
    </div>
    <div>
        <label asp-for="Password"></label>
        <input asp-for="Password" value="Wgh123123!"/>
        <span asp-validation-for="Password"></span>
    </div>
    <div>
        <label asp-for="RememberMe"></label>
        <input asp-for="RememberMe"/>
        <span asp-validation-for="RememberMe"></span>
    </div>
    <input type="submit" value="Login"/>
</form>
```
## Apply authentication to HomeController

Put `[Authorize]` tag to the controller. We want any user can access Home/Index, so put `[AllowAnonymous]` there.

```csharp
using Aspnetcore.Fundamentals.Models;
using Aspnetcore.Fundamentals.Services;
using Aspnetcore.Fundamentals.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aspnetcore.Fundamentals.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IGreeter _greeter;
        private readonly IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Restaurants = _restaurantData.GetAll(),
                CurrentMessage = _greeter.GetGreeting()
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // particularly a form POST, is very important when you are authenticating users using cookies.
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var newRestaurant = new Restaurant
            {
                Cuisine = model.Cuisine,
                Name = model.Name
            };
            newRestaurant = _restaurantData.Add(newRestaurant);
            _restaurantData.Commit(); // business layer determines when to interact with database

            return RedirectToAction("Details", new {id = newRestaurant.Id});
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RestaurantEditViewModel model)
        {
            var restaurant = _restaurantData.Get(id);

            if (!ModelState.IsValid) return View(restaurant);

            restaurant.Cuisine = model.Cuisine;
            restaurant.Name = model.Name;
            _restaurantData.Commit();
            return RedirectToAction(nameof(Details), new {id = restaurant.Id});
        }
    }
}
```

## Migration

```bash
dotnet ef migrations add Identity
dotnet ef database update
```

Now we're good to run the project!