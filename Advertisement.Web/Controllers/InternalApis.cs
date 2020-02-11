using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Advertisement.Web.ServiceClients;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Advertisement.Web.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class InternalApis : Controller
    {
        private readonly IAdvertisementClient _AdvertisementClient;

        public InternalApis(IAdvertisementClient AdvertisementClient)
        {
            _AdvertisementClient = AdvertisementClient;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(string id)
        {
            var record = await _AdvertisementClient.GetAsync(id).ConfigureAwait(false);
            return Json(record);
        }
    }
}