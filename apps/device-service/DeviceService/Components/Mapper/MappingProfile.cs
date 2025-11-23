using AutoMapper;
using DeviceService.DAL.Entity;
using DeviceService.Models;

namespace DeviceService.Components.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Device, DeviceInfo>()
                .ForMember(di => di.AvailableCommands, (opt) => opt.MapFrom(src => src.Actions.Select(a => a.Name)))
                .ForMember(di => di.AvailableMetrics,
                    (opt) => opt.MapFrom(src => src.AvailableMetrics.Select(a => a.Name)))
                .ForMember(di => di.DeviceType, (opt) => opt.MapFrom(src => src.Type.Name));

        }
    }
}