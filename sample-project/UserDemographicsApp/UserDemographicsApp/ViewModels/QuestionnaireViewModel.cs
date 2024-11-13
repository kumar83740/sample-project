using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using UserDemographicsApp.Models;

namespace UserDemographicsApp.ViewModels
{
    public class QuestionnaireViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserEmailId { get; set; }

        public List<Question>? Questions { get; set; }

        public List<Option>? Options { get; set; }

        public List<Country>? Countries { get; set; }

        public List<Language>? Languages { get; set; }
        public List<State>? States { get; set; }

        [Required]
        public Dictionary<int, string> Responses { get; set; }

        [Required]
        public int? SelectedCountryId { get; set; }
        public Dictionary<int, string> AdditionalTexts { get; set; }
        public List<int> SelectedLanguageIds { get; set; } = new List<int>();
        public int? StateID { get; set; }

        public QuestionnaireViewModel()
        {
            Responses = new Dictionary<int, string>();
            AdditionalTexts = new Dictionary<int, string>();
        }
    }

}
