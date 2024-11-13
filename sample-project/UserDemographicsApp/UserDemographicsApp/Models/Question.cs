namespace UserDemographicsApp.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
