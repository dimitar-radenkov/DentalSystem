namespace DentalSystem.Web
{
    using DentalSystem.Web.Extensions;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static void Main(string[] args) => 
            CreateWebHostBuilder(args)
                .Build()
                .SeedDatabase()
                .Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
