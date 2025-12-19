using AutoMapper;
using Practical_17.Models;
using Practical_17.ViewModels;

namespace Practical_17.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Students, StudentListViewModel>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)
                 );

            CreateMap<StudentListViewModel, Students>()
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore());


            CreateMap<Students, StudentDetailsViewModel>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)
                 );

            CreateMap<StudentDetailsViewModel, Students>()
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore());

            CreateMap<StudentCreateViewModel, Students>().ReverseMap();
            CreateMap<StudentEditViewModel, Students>().ReverseMap();
            CreateMap<StudentDeleteViewModel, Students>().ReverseMap();

        }
    }
}
