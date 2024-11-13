namespace UserDemographicsApp.Models
{
    public class Response
    {
        public int ResponseId { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public int? OptionId { get; set; }
        public string AdditionalText { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string SelectedLanguageIds { get; set; }
        public Question Question { get; set; }
        public Option Option { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
    }
}
