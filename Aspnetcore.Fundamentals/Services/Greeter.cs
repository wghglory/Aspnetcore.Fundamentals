using System;
using Microsoft.Extensions.Configuration;

namespace Aspnetcore.Fundamentals.Services
{

    public interface IGreeter
    {
        string GetGreeting();
    }

    public class Greeter : IGreeter
    {
        private readonly string _greeting;

        public Greeter(IConfiguration configuration)
        {
            _greeting = configuration["Greeting"];
        }

        public string GetGreeting()
        {
            return _greeting;
        }
    }
}
