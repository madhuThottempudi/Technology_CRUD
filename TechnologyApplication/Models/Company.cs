using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechnologyApplication.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Com_Name { get; set; }
        public string Comp_Type { get; set; }
        public string OwnerName { get; set; }
        public int Comp_est { get; set; }
    }
}
