using Microsoft.AspNetCore.Mvc;
using UserDemographicsApp.Models;
using UserDemographicsApp.Services;
using UserDemographicsApp.ViewModels;

public class QuestionnaireController : Controller
{
    private readonly IQuestionnaireService _service;

    public QuestionnaireController(IQuestionnaireService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(string userId)
    {
        //if (string.IsNullOrWhiteSpace(userId) || userId.Length > 50)
        //{
        //    return BadRequest("Invalid user ID.");
        //}

      
        if (!string.IsNullOrEmpty(userId)) {
            // Check if the user has already submitted the survey
            bool hasSubmitted = await _service.HasUserSubmittedSurveyAsync(userId);

            if (hasSubmitted)
            {
                // Display a message that the user has already submitted the survey
                ViewBag.Message = "You have already submitted the survey.";
                return View("SurveyAlreadySubmitted");
            }
        }
       

        var questions = await _service.GetQuestionsAsync();
        var options = await _service.GetOptionsAsync();
        var countries = await _service.GetCountriesAsync();
        var states = await _service.GetStatesAsync();
        var languages = await _service.GetLanguagesAsync();

        var viewModel = new QuestionnaireViewModel
        {
            Questions = questions,
            Options = options,
            Countries = countries,
            States = states,
            Languages = languages,
            UserId = userId ?? "xx-xx"
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Submit(QuestionnaireViewModel viewModel)
    {
        if (string.IsNullOrEmpty(viewModel.UserId) || viewModel.UserId == "xx-xx")
        {
            viewModel.UserId = viewModel.UserEmailId;
        }

        // Check if the user has already submitted the survey
        bool hasSubmitted = await _service.HasUserSubmittedSurveyAsync(viewModel.UserId);

        if (hasSubmitted)
        {
            // Display a message that the user has already submitted the survey
            ViewBag.Message = "You have already submitted the survey.";
            return View("SurveyAlreadySubmitted");
        }

        if (!ModelState.IsValid)
        {
            // Log validation errors or inspect them
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                // Log or inspect the error messages
                Console.WriteLine(error.ErrorMessage);
            }

            // Reload data for the view
            viewModel.Questions = await _service.GetQuestionsAsync();
            viewModel.Options = await _service.GetOptionsAsync();
            viewModel.Countries = await _service.GetCountriesAsync();
            viewModel.States = await _service.GetStatesAsync();
            viewModel.Languages = await _service.GetLanguagesAsync();
            return View("Index", viewModel);
        }

        await _service.SaveResponsesAsync(viewModel);
        return RedirectToAction("ThankYou");
    }


    public IActionResult ThankYou()
    {
        return View();
    }
}
