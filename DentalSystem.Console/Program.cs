using AutoMapper;
using DentalSystem.Data;
using DentalSystem.Web.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DentalSystem.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile(new AutoMapperProfile());
            });

            var db = new DentalSystemDbContext(new DbContextOptions<DentalSystemDbContext>());
            
            
        }
    }
}
