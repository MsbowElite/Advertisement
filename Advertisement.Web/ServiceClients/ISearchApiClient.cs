using System.Collections.Generic;
using System.Threading.Tasks;
using Advertisement.Web.Models;

namespace Advertisement.Web.ServiceClients
{
    public interface ISearchApiClient
    {
        Task<List<AdvertType>> Search(string keyword);
    }
}
