## Project Setup

1. Create a repo in github with README.md and LICENSE
1. Clone the repo to local folder `Aspdotnetcore.Fundamentals`
1. Using VS 2017 for mac to create an empty .net core web project `Aspdotnetcore.Fundamentals` with git and gitignore
1. `git pull` so LICENSE and README is merged into project

## Project Structure

```
|-- Aspnetcore.Fundamentals
    |-- Aspnetcore.Fundamentals.csproj
    |-- Aspnetcore.Fundamentals.csproj.user
    |-- Program.cs
    |-- Startup.cs
    |-- wwwroot
```

###### Program.cs

```csharp
public static void Main(string[] args)
{
    var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

    host.Run();
}
```

###### Startup.cs

```csharp
public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.Run(async (context) =>
        {
            await context.Response.WriteAsync("Hello World!");
        });
    }
}
```

###### wwwroot folder

To server for the static files

## Running

Nuget packages should be restored automatically and you will see "Hello World!" after running it.

## [.net core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/)

Basic commands

```bash
new
restore
build
publish
run
test
vstest
pack
migrate
clean
sln
```

Project modification commands

```
add package
add reference
remove package
remove reference
list reference
```

Advanced commands

```
nuget delete
nuget locals
nuget push
msbuild
dotnet install script
```

