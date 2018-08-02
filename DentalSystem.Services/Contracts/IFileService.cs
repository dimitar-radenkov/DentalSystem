namespace DentalSystem.Services.Contracts
{
    using DentalSystem.Models.ViewModels;

    public interface IFileService
    {
        FileViewModel GetById(string id);
    }
}
