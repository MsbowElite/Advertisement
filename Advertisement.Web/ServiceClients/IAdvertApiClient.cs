using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advertisement.Web.ServiceClients
{
    public interface IAdvertisementClient
    {
        Task<AdvertResponse> CreateAsync(CreateAdvertModel model);
        Task<bool> ConfirmAsync(ConfirmAdvertRequest model);
        Task<List<Advert>> GetAllAsync();
        Task<Advert> GetAsync(string advertId);
    }
}
