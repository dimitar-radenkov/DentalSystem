namespace DentalSystem.Web.Controllers
{
    using System.IO;
    using DentalSystem.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService fileService;
  
        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet("{id}")]
        public FileStreamResult Get(string id)
        {
            var file = this.fileService.GetById(id);
            if (file == null)
            {
                throw new FileNotFoundException(@"unable to find file with ID: {id}");
            }

            return new FileStreamResult(
                new MemoryStream(file.Data),
                file.ContentType);
        }
    }
}