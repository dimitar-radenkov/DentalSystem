using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DentalSystem.Web.Areas.Identity.IdentityHostingStartup))]
namespace DentalSystem.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => 
            {

            });
        }
    }
}