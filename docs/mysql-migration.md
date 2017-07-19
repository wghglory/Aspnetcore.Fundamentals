# Connecting .Net core application to MySql

- Microsoft.AspNetCore
- Microsoft.AspNetCore.Mvc
- Microsoft.AspNetCore.StaticFiles
- Microsoft.NETCORE.App
- MongoDB.Driver
- **Microsoft.EntityFrameworkCore.Tools** (for migration)
- **Microsoft.EntityFrameworkCore.Tools.DotNet** (for dotnet ef)
- **MySql.Data.EntityFrameworkCore** (official)
- // Pomelo.EntityFrameworkCore.MySql (3rd party library)

## Code First, Migration

### Add connection string

appsettings.json

```json
{
  "ConnectionStrings": {
    // "MssqlConnection": "Server=(localdb)\\MSSQLLocalDB;Database=_CHANGE_ME;Trusted_Connection=True;MultipleActiveResultSets=true"    //sql server
    "MysqlConnection": "Server=localhost;Database=Food;userid=derek;pwd=derek;port=3306;sslmode=none;Character Set=utf8"
  }
}
```

### Create a DbContext

Models/FoodDbContext.cs

```csharp
using Microsoft.EntityFrameworkCore;

namespace Aspnetcore.Fundamentals.Models
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
```

### Create a Service

```csharp
using Aspnetcore.Fundamentals.Models;

namespace Aspnetcore.Fundamentals.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant newRestaurant);
    }

    public class MySqlRestaurantData : IRestaurantData
    {
        private readonly FoodDbContext _context;

        public MySqlRestaurantData(FoodDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            _context.SaveChanges();
            return newRestaurant;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
    }
}
```

### Configure Service and DbContext

```csharp
using Aspnetcore.Fundamentals.Models;
using Aspnetcore.Fundamentals.Services;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace Aspnetcore.Fundamentals
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // ...
            
            services.AddScoped<IRestaurantData, MySqlRestaurantData>();

            services.AddDbContext<FoodDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("MysqlConnection")));
        }
    }
}
```

### It's time to run migration!

#### Commands for Package Manager Console

```bash
Add-Migration <name>: 执行此命令项目生成一个目录Migration

Remove-Migration

Update-Database  # applying a migration
```

#### Commands for CLI

Modify `.csproj` file and manually add `<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.1" />` if necessary

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore" Version="1.1.2" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="1.1.3" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="1.1.2" />
    <PackageReference Include="MongoDB.Driver" Version="2.4.4" />
    
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="1.1.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.1" />
    <PackageReference Include="MySql.Data.EntityFrameworkCore" Version="8.0.8-dmr" />
  </ItemGroup>
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.1" />
  </ItemGroup>
```

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet

dotnet ef migrations add <name>

dotnet ef migrations remove

dotnet ef database update  # applying a migration
```

After `dotnet ef migrations add InitCreate` and `dotnet ef database update`, Mysql should have a new database called `Food` with `__EFMigrationsHistory` and `Restaurants` table.

Then we should be able to run our project successfully!

### More about migration 

http://www.learnentityframeworkcore.com/migrations