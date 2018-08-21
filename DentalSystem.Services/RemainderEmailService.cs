namespace DentalSystem.Services
{
    using System.Net;
    using System.Net.Mail;
    using DentalSystem.Services.Contracts;

    public class RemainderEmailService : IEmailService
    {
        public const string SENDER = "dimitar.radenkov1@gmail.com";

        public void Send(string receiver, string body)
        {
            
        }
    }
}
