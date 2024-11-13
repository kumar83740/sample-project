using UserDemographicsApp.Models;
using UserDemographicsApp.ViewModels;

namespace UserDemographicsApp.Services
{
    public interface IQuestionnaireService
    {
        Task<List<Question>> GetQuestionsAsync();
        Task<List<Option>> GetOptionsAsync();
        Task<List<Country>> GetCountriesAsync();
        Task SaveResponsesAsync(QuestionnaireViewModel viewModel);
        Task<List<Response>> GetResponsesAsync();
        Task<List<Language>> GetLanguagesAsync();
        Task<List<State>> GetStatesAsync();
        Task<bool> HasUserSubmittedSurveyAsync(string userId);
    }
}
