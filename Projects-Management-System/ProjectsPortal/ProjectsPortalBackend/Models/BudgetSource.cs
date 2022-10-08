using System.ComponentModel.DataAnnotations;

namespace ProjectsPortalBackend.Models
{
    public class BudgetSource
    {
        [Key]
        public int BudgetSourceID {get; set;}
        public string BudgetSourceName {get; set;}
    }
}
