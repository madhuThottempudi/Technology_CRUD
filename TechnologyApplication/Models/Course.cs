using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnologyApplication.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }
        public string CourseName { get;set; }
        public string CourseType { get; set; }
        public string CourseDuration { get; set; }
        public int CourseCost { get; set; }
      
    
    }
}
