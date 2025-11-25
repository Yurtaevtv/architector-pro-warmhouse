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
                .ForMember(di => di.Actions, (opt) => opt.MapFrom(src => src.Actions.Select(a => a.Name)))
                .ForMember(di => di.Metrics,
                    (opt) => opt.MapFrom(src => src.DeviceMetrics.ToDictionary(dm => dm.Metric.Name, dm => dm.Value)))
                .ForMember(di => di.DeviceType, (opt) => opt.MapFrom(src => src.Type.Name));

        }
    }
}