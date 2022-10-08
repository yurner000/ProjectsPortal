using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsPortalBackend.Models
{
    public class BusinessUnit
    {
        [Key]
        public int BusinessUnitID {get; set;}

        [ForeignKey("District")]
        public int DistrictID {get; set;}
        public string BusinessUnitName {get; set;}
        public virtual District District {get; set;}
    }
}
