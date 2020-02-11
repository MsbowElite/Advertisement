using Advertisement.Models;
using AutoMapper;
using Advertisement.Web.Models;
using Advertisement.Web.Models.AdvertManagement;
using Advertisement.Web.Models.Home;
using Advertisement.Web.ServiceClients;

namespace Advertisement.Web.Classes
{
    public class WebsiteProfiles : Profile
    {
        public WebsiteProfiles()
        {
            CreateMap<CreateAdvertModel, CreateAdvertViewModel>().ReverseMap();

            CreateMap<AdvertModel, Advert>().ReverseMap();

            CreateMap<Advert, IndexViewModel>()
                .ForMember(
                    dest => dest.Title, src => src.MapFrom(field => field.Title))
                .ForMember(dest=>dest.Image, src=> src.MapFrom(field=>field.FilePath));

            CreateMap<AdvertType, SearchViewModel>()
                .ForMember(
                    dest => dest.Id, src => src.MapFrom(field => field.Id))
                .ForMember(
                    dest => dest.Title, src => src.MapFrom(field => field.Title));
        }
    }
}
