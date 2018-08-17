namespace DentalSystem.Data.DbInitializer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DentalSystem.Common.Contants;
    using DentalSystem.Models;
    using Microsoft.AspNetCore.Identity;

    public static class DbInitializer
    {
        public static string ADMIN_PASS = "admin123";
        public static string ADMIN_EMAIL = "admin@gmail.com";

        public static string OFFICE_MANAGER_PASS = "office123";
        public static string OFFICE_MANAGER_EMAIL = "office@gmail.com";
     
        public static string DEFAULT_PASS = "qwerty";

        public static void Initialize(
            DentalSystemDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (context.Manipulations.Any())
            {
                //db is already seeded.
                return;
            }

            roleManager.CreateAsync(new IdentityRole(Roles.ADMINISTRATOR)).Wait();
            roleManager.CreateAsync(new IdentityRole(Roles.OFFICE_MANAGER)).Wait();

            AddManipulations(context);
            AddAdmin(context, userManager);
            AddOfficeManager(context, userManager);
            AddDoctors(context, userManager);
            AddPatients(context, userManager, 1000);
            AddAppointments(context, userManager, 500);
        }


        private static void AddManipulations(DentalSystemDbContext context)
        {
            context.Manipulations.AddRange(
                new Manipulation {  Name = "Teeth Whitening", Duration = TimeSpan.FromMinutes(40), Price = 80 },
                new Manipulation {  Name = "Bonding", Duration = TimeSpan.FromMinutes(90), Price = 150 },
                new Manipulation {  Name = "Braces", Duration = TimeSpan.FromMinutes(60), Price = 100 },
                new Manipulation {  Name = "Extractions ", Duration = TimeSpan.FromMinutes(30), Price = 50 },
                new Manipulation {  Name = "Root Canals", Duration = TimeSpan.FromMinutes(120), Price = 300 });
            context.SaveChanges();
        }

        private static void AddAdmin(
            DentalSystemDbContext context,
            UserManager<User> userManager)
        {
            var admin = new User
            {            
                UserName = ADMIN_EMAIL,
                Email = ADMIN_EMAIL,
                Type = UserType.Admin,
                EmailConfirmed = true,
            };

            userManager.CreateAsync(admin, ADMIN_PASS).Wait();
            userManager.AddToRoleAsync(admin, Roles.ADMINISTRATOR).Wait();
        }

        private static void AddOfficeManager(
            DentalSystemDbContext context,
            UserManager<User> userManager)
        {
            var officeManager = new User
            {
                UserName = OFFICE_MANAGER_EMAIL,
                Email = OFFICE_MANAGER_EMAIL,
                Name = "Office Manager",
                Type = UserType.OfficeManager,
                EmailConfirmed = true,
            };

            userManager.CreateAsync(officeManager, OFFICE_MANAGER_PASS).Wait();
            userManager.AddToRoleAsync(officeManager, Roles.OFFICE_MANAGER).Wait();
        }

        private static void AddDoctors(
            DentalSystemDbContext context, 
            UserManager<User> userManager)
        {
            var current = System.IO.Directory.GetCurrentDirectory();
            var imagePaths = System.IO.Directory.EnumerateFiles(
                $"{System.IO.Directory.GetParent(current)}\\assets".ToString())
                .ToList();

            for (int i = 0; i < 4; ++i)
            {
                var file = new File
                {
                    Data = System.IO.File.ReadAllBytes(imagePaths[i]),
                    ContentType = "image/png"
                };
                context.Files.Add(file);
                context.SaveChanges();

                var email = Faker.Internet.Email();
                var doctor = new User
                {
                    File = file,
                    UserName = email,
                    Email = email,
                    Type = UserType.Doctor,
                    PhoneNumber = Faker.Phone.Number(),
                    Name = Faker.Name.FullName(),
                    EmailConfirmed = true,
                };

                userManager.CreateAsync(doctor, DEFAULT_PASS).Wait();               
            }
        }


        private static void AddPatients(
            DentalSystemDbContext context, 
            UserManager<User> userManager,
            int count)
        {
            for (int i = 0; i < count; ++i)
            {
                var email = Faker.Internet.Email();
                userManager.CreateAsync(
                    new User
                    {
                        Name = Faker.Name.FullName(),
                        Email = email,
                        UserName = email,
                        PhoneNumber = Faker.Phone.Number(),
                        Type = UserType.Ordinary
                    },
                    DEFAULT_PASS).Wait();
            }
        }


        private static void AddAppointments(
            DentalSystemDbContext context,
            UserManager<User> userManager,
            int count)
        {
            var random = new Random();

            var patients = context.Users
                .Where(x => x.Type == UserType.Ordinary)
                .ToList();

            var doctors = context.Users
                .Where(x => x.Type == UserType.Doctor)
                .ToList();

            var manupilations = context.Manipulations.ToList();

            for (int i = 0; i < count; ++i)
            {
                var patient = patients[random.Next(patients.Count)];
                var doctor = doctors[random.Next(doctors.Count)];

                DateTime dateTime = DateTime.Now;

                var num = random.Next();
                if (num % 2 == 0)
                {
                    dateTime = dateTime.AddDays(random.Next(10, 1000));
                }
                else 
                {
                    dateTime = dateTime.Subtract(TimeSpan.FromDays(random.Next(10, 1000)));
                }

                var appointment = new Appointment()
                {
                    User = patient,
                    Doctor = doctor,
                    DateTime = dateTime,
                    Status = num % 7 == 0 
                        ? AppointmentStatus.Canceled 
                        : dateTime > DateTime.Now
                            ? AppointmentStatus.Scheduled 
                            : AppointmentStatus.Done,
                };
                context.Appointments.Add(appointment);

                var manipulatioIds = Enumerable
                    .Range(1, 4)
                    .Select(x => random.Next(manupilations.Count))
                    .Distinct();

                appointment.Manipulations = manipulatioIds
                    .Select(mId => new ManipulationAppointment
                    {
                        Appointment = appointment,
                        Manipulation = manupilations.ToList()[mId]
                    })
                    .ToList();

                context.ManipulationsAppointments.AddRange(appointment.Manipulations);
                context.SaveChanges();
            }
        }
    }
}
