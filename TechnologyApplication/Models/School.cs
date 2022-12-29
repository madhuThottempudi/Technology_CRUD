using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechnologyApplication.Models
{
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SchoolId { get; set; }
        [Required]
        public string SchoolName { get; set; }

        [Column("School_Add", Order = 5)]
        public string SchoolAddress { get; set; }

        public string SchoolType { get; set; }

        [NotMapped]
        public string SchoolLocation { get; set; }

        [ForeignKey("Id")]
        public int StudentID { get; set; }
        public Student Student {get; set;}
       
    }
}
