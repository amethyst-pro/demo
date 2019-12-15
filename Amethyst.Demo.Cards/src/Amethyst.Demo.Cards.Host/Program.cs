using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Amethyst.Demo.Cards.Host
{
    public static class Program
    {
        public static void Main(string[] args)
            => CreateWebHostBuilder(args)
                .Build()
                .Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}