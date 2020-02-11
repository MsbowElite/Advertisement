using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Advertisement.Web.Models;
using Advertisement.Web.Models.Home;
using Advertisement.Web.ServiceClients;

namespace Advertisement.Web.Controllers
{
    public class HomeController : Controller
    {
        public ISearchApiClient SearchApiClient { get; }
        public IMapper Mapper { get; }
        public IAdvertisementClient ApiClient { get; }

        public HomeController(ISearchApiClient searchApiClient, IMapper mapper, IAdvertisementClient apiClient)
        {
            SearchApiClient = searchApiClient;
            Mapper = mapper;
            ApiClient = apiClient;
        }

        [Authorize]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Index()
        {
            var allAds = await ApiClient.GetAllAsync().ConfigureAwait(false);
            var allViewModels = allAds.Select(x => Mapper.Map<IndexViewModel>(x));

            return View(allViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keyword)
        {
            var viewModel = new List<SearchViewModel>();

            var searchResult = await SearchApiClient.Search(keyword).ConfigureAwait(false);
            searchResult.ForEach(advertDoc =>
            {
                var viewModelItem = Mapper.Map<SearchViewModel>(advertDoc);
                viewModel.Add(viewModelItem);
            });

            return View("Search", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}