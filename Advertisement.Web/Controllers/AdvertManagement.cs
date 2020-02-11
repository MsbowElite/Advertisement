using System;
using System.IO;
using System.Threading.Tasks;
using Advertisement.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Advertisement.Web.Models.AdvertManagement;
using Advertisement.Web.ServiceClients;
using Advertisement.Web.Services;


namespace Advertisement.Web.Controllers
{
    public class AdvertManagementController : Controller
    {
        private readonly IFileUploader _fileUploader;
        private readonly IAdvertisementClient _AdvertisementClient;
        private readonly IMapper _mapper;

        public AdvertManagementController(IFileUploader fileUploader, IAdvertisementClient AdvertisementClient, IMapper mapper)
        {
            _fileUploader = fileUploader;
            _AdvertisementClient = AdvertisementClient;
            _mapper = mapper;
        }

        public IActionResult Create(CreateAdvertViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertViewModel model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var createAdvertModel = _mapper.Map<CreateAdvertModel>(model);
                createAdvertModel.UserName = User.Identity.Name;

                var apiCallResponse = await _AdvertisementClient.CreateAsync(createAdvertModel).ConfigureAwait(false);
                var id = apiCallResponse.Id;

                bool isOkToConfirmAd = true;
                string filePath = string.Empty; 
                if (imageFile != null)
                {
                    var fileName = !string.IsNullOrEmpty(imageFile.FileName) ? Path.GetFileName(imageFile.FileName) : id;
                    filePath = $"{id}/{fileName}";

                    try
                    {
                        using (var readStream = imageFile.OpenReadStream())
                        {
                            var result = await _fileUploader.UploadFileAsync(filePath, readStream)
                                .ConfigureAwait(false);
                            if (!result)
                                throw new Exception(
                                    "Could not upload the image to file repository. Please see the logs for details.");
                        }
                    }
                    catch (Exception e)
                    {
                        isOkToConfirmAd = false;
                        var confirmModel = new ConfirmAdvertRequest()
                        {
                            Id = id,
                            FilePath = filePath,
                            Status = AdvertStatus.Pending
                        };
                        await _AdvertisementClient.ConfirmAsync(confirmModel).ConfigureAwait(false);
                        Console.WriteLine(e);
                    }


                }

                if (isOkToConfirmAd)
                {
                    var confirmModel = new ConfirmAdvertRequest()
                    {
                        Id = id,
                        FilePath = filePath,
                        Status = AdvertStatus.Active
                    };
                    await _AdvertisementClient.ConfirmAsync(confirmModel).ConfigureAwait(false);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}