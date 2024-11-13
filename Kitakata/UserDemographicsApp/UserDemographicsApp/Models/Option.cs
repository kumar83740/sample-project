namespace UserDemographicsApp.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public int QuestionId { get; set; }
        public string? OptionInfo { get; set; }
        public Question Question { get; set; }
    }
}
