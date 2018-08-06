namespace DentalSystem.Web.Mapping
{
    using System.Linq;
    using AutoMapper;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<File, FileViewModel>();

            this.CreateMap<User, DoctorViewModel>()
                .ForMember(vm => vm.Name, cfg => cfg.MapFrom(user => user.Name))
                .ForMember(vm => vm.Email, cfg => cfg.MapFrom(user => user.Email))
                .ForMember(vm => vm.Phone, cfg => cfg.MapFrom(user => user.PhoneNumber));

            this.CreateMap<User, PatientViewModel>()
                .ForMember(vm => vm.Name, cfg => cfg.MapFrom(user => user.Name));

            this.CreateMap<Manipulation, ManipulationViewModel>();

            this.CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(vm => vm.DoctorName, cfg => cfg.MapFrom(vm => vm.Doctor.Name))
                .ForMember(vm => vm.PatientName, cfg => cfg.MapFrom(vm => vm.User.Name))
                .ForMember(vm => vm.Manupulations, cfg => cfg.MapFrom(vm => vm.Manipulations.Select(m => m.Manipulation)));

        }
    }
}
