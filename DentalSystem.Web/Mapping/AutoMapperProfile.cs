namespace DentalSystem.Web.Mapping
{
    using AutoMapper;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<File, FileViewModel>();

            this.CreateMap<Doctor, DoctorViewModel>()
                .ForMember(vm => vm.Name, cfg => cfg.MapFrom(model => model.User.UserName))
                .ForMember(vm => vm.Email, cfg => cfg.MapFrom(model => model.User.Email))
                .ForMember(vm => vm.Phone, cfg => cfg.MapFrom(model => model.User.PhoneNumber));
        }
    }
}
