using System.Collections.Generic;
using System.Threading.Tasks;
using Advertisement.SearchApi.Models;

namespace Advertisement.SearchApi.Services
{
    public interface ISearchService
    {
        Task<List<AdvertType>> Search(string keyword);
    }
}
