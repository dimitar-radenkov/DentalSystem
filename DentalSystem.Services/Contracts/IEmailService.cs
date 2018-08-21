namespace DentalSystem.Services.Contracts
{
    public interface IEmailService
    {
        void Send(string receiver, string body);
    }
}
