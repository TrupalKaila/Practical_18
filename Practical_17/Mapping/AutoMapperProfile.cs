using AutoMapper;
using Practical_17.Models;
using Practical_17.ViewModels;

namespace Practical_17.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Students, StudentViewModel>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)
                 );

            CreateMap<StudentViewModel, Students>()
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore());
        }
    }
}
