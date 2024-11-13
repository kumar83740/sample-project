using Microsoft.EntityFrameworkCore;
using UserDemographicsApp.Common;
using UserDemographicsApp.Models;
using UserDemographicsApp.Services;
using UserDemographicsApp.ViewModels;

public class QuestionnaireService : IQuestionnaireService
{
    private readonly ApplicationDbContext _context;

    public QuestionnaireService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetQuestionsAsync()
    {
        return await _context.Questions.Include(q => q.Options).ToListAsync();
    }

    public async Task<List<Option>> GetOptionsAsync()
    {
        return await _context.Options.ToListAsync();
    }

    public async Task<List<Country>> GetCountriesAsync()
    {
        return await _context.Countries.OrderBy(x=> x.CountryName).ToListAsync();
    }

    public async Task<List<Language>> GetLanguagesAsync()
    {
        return await _context.Languages.OrderBy(x=> x.Name).OrderBy(x=> x.Name).ToListAsync();
    }

    public async Task<List<State>> GetStatesAsync()
    {
        return await _context.States.OrderBy(x=> x.StateName).ToListAsync();
    }

    public async Task<List<Response>> GetResponsesAsync()
    {
        var responses = await _context.Responses.ToListAsync();

        foreach (var response in responses)
        {
            response.UserId = EncryptionHelper.Decrypt(response.UserId);
        }

        return responses;
    }

    public async Task<bool> HasUserSubmittedSurveyAsync(string userId)
    {
        var encryptedResponses = await _context.Responses
                                               .Select(r => r.UserId)
                                               .ToListAsync();

        foreach (var encryptedUserId in encryptedResponses)
        {
            if (EncryptionHelper.Decrypt(encryptedUserId) == userId)
            {
                return true;
            }
        }

        return false;
    }


    public async Task SaveResponsesAsync(QuestionnaireViewModel viewModel)
    {
        var responses = new List<Response>();
        if (string.IsNullOrEmpty(viewModel.UserId)) {
            viewModel.UserId= viewModel.UserEmailId;
        }
        string encryptedUserId = EncryptionHelper.Encrypt(viewModel.UserId);

        foreach (var res in viewModel.Responses)
        {
            // Prepare the response object
            var newResponse = new Response
            {
                UserId = encryptedUserId,
                QuestionId = res.Key,
                CountryId = viewModel.SelectedCountryId,
                AdditionalText = viewModel.AdditionalTexts.ContainsKey(res.Key) ? viewModel.AdditionalTexts[res.Key] : null,
                StateId = viewModel.StateID,
                SelectedLanguageIds = string.Join(",", viewModel.SelectedLanguageIds)
            };

            // Check if the response value is an option ID or additional text
            if (int.TryParse(res.Value, out int optionId))
            {
                newResponse.OptionId = optionId;
            }

            responses.Add(newResponse);
        }

        // Add all responses to the context
        _context.Responses.AddRange(responses);

        // Save changes to the database
        await _context.SaveChangesAsync();
    }

}
