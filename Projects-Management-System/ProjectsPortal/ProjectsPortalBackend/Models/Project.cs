using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsPortalBackend.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [ForeignKey("User")]
        public int UserID {get; set;}

        [ForeignKey("BusinessUnit")]
        public int BusinessUnitID { get; set; }
        public int Budget { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual User User { get; set; }
    }
}
