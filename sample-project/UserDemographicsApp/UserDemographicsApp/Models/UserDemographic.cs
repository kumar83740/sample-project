using System.ComponentModel.DataAnnotations;

namespace UserDemographicsApp.Models
{
    public class UserDemographic
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }
    }
}
