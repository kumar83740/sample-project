namespace UserDemographicsApp.ViewModels
{
    public class ResponseViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int? OptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public string AdditionalText { get; set; } = string.Empty;
        public int? CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public int? StateID { get; set; }
        public string StateName { get; set; } = string.Empty;
        public string SelectedLanguageIds { get; set; } = string.Empty;
        public string? SelectedLanguages { get; set; }
    }
}
