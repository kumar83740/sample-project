using Microsoft.EntityFrameworkCore;
using UserDemographicsApp.Common;
using UserDemographicsApp.Services;
using UserDemographicsApp.ViewModels;

public class ResponseService: IResponseService
{
    private readonly ApplicationDbContext _context;

    public ResponseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ResponseViewModel>> GetAnonymousResponsesAsync()
    {
        var responses = await GetResponsesWithLanguagesAsync();
        foreach (var response in responses)
        {
            response.UserName = "Anonymous";
        }

        return responses;
    }

    public async Task<IEnumerable<ResponseViewModel>> GetNamedResponsesAsync()
    {
        var responses = await GetResponsesWithLanguagesAsync();
        foreach (var response in responses)
        {
            response.UserName = EncryptionHelper.Decrypt(response.UserName);
        }

        return responses;
    }

    private async Task<IEnumerable<ResponseViewModel>> GetResponsesWithLanguagesAsync()
    {
        var responses = await _context.ResponseViewModel.FromSqlRaw("EXEC GetResponses").ToListAsync();
        var languageIds = responses.SelectMany(r => r.SelectedLanguageIds.Split(',').Select(int.Parse)).Distinct();
        var languages = await _context.Languages.Where(l => languageIds.Contains(l.LanguageId)).ToListAsync();

        foreach (var response in responses)
        {
            var languageNames = response.SelectedLanguageIds.Split(',')
                .Select(id => languages.FirstOrDefault(l => l.LanguageId == int.Parse(id))?.Name)
                .Where(name => name != null);

            response.SelectedLanguages = string.Join(", ", languageNames);
        }

        return responses;
    }
}
