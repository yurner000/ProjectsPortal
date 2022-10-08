using System.ComponentModel.DataAnnotations;

namespace ProjectsPortalBackend.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserFirstname { get; set; }
        public string UserSecondname { get; set; }
        public string UserEmail { get; set; }
        public bool UserRole { get; set; } //true: admin / false: user
        public bool IsActive { get; set; }
        public string UserDepartment { get; set; }
        public string UserPassword { get; set; }

    }
}
