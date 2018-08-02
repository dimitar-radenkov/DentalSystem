namespace DentalSystem.Services.Contracts
{
    using System.Linq;
    using AutoMapper;
    using DentalSystem.Data;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;

    public class FileService : IFileService
    {
        private readonly DentalSystemDbContext db;
        private readonly IMapper mapper;

        public FileService(DentalSystemDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public FileViewModel GetById(string id) =>
            this.mapper.Map<File, FileViewModel>(this.db.Files.FirstOrDefault(f => f.Id.ToString() == id));
    }
}
