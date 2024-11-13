using UserDemographicsApp.ViewModels;

namespace UserDemographicsApp.Services
{
    public interface IResponseService
    {
        Task<IEnumerable<ResponseViewModel>> GetAnonymousResponsesAsync();
        Task<IEnumerable<ResponseViewModel>> GetNamedResponsesAsync();
    }
}
