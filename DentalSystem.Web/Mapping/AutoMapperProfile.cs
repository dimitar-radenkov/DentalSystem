namespace DentalSystem.Web.Mapping
{
    using AutoMapper;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Doctor, DoctorViewModel>();
        }
    }
}
