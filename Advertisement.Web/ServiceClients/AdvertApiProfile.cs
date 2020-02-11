using Advertisement.Models;
using AutoMapper;

namespace Advertisement.Web.ServiceClients
{
    public class AdvertisementProfile : Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<AdvertModel, CreateAdvertModel>().ReverseMap();
            CreateMap<CreateAdvertResponse, AdvertResponse>().ReverseMap();
            CreateMap<ConfirmAdvertRequest, ConfirmAdvertModel>().ReverseMap();
        }
    }
}
