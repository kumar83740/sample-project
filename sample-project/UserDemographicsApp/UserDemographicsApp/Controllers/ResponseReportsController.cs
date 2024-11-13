using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserDemographicsApp.Services;
using UserDemographicsApp.ViewModels;

namespace UserDemographicsApp.Controllers
{
    public class ResponseReportsController : Controller
    {
        private readonly IResponseService _responseService;

        public ResponseReportsController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        public async Task<IActionResult> Anonymous()
        {
            var responses = await _responseService.GetAnonymousResponsesAsync();
            return View(responses);
        }

        public async Task<IActionResult> Named()
        {
            var responses = await _responseService.GetAnonymousResponsesAsync();
            return View(responses);
        }
    }
}
