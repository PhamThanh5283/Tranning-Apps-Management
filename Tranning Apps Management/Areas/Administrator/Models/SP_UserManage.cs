using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;

namespace Tranning_Apps_Management.Areas.Administrator.Models
{
    public class SP_UserManage
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string DeptID { get; set;}
        public string DeptName { get; set;}
    }
    public class UserManage
    {
        public IEnumerable<SP_UserManage> Usermanage { get; set; }
        public IEnumerable<Tranning_Apps_Management.Models.DB.Department> departments { get; set; }
    }

}
