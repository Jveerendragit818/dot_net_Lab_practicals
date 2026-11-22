using System.ComponentModel.DataAnnotations;

namespace Experiment_10.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Department { get; set; }

        [Range(1, 8)]
        public int Semester { get; set; }
    }


}
