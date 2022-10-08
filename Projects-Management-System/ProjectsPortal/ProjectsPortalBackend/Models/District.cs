using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsPortalBackend.Models
{
    public class District
    {
        [Key]
        public int DistrictID {get; set;}
        public string DistrictName {get; set;}

        [ForeignKey("BudgetSource")]
        public int BudgetSourceID {get; set;}

        public virtual BudgetSource BudgetSource {get; set;}
    }
}
