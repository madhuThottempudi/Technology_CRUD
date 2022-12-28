using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechnologyApplication.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GradeId { get; set; }
        [Required]
        public string GradeName { get; set; }
        public string Section { get; set; }
    }
}
