using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Tranning_Apps_Management.Areas.Administrator.Models;
using Tranning_Apps_Management.Controllers;
using Tranning_Apps_Management.Models;
using Tranning_Apps_Management.Models.DB;
using Tranning_Apps_Management.Utils;

namespace Tranning_Apps_Management.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("UsersAdmin")]
    public class UsersAdminController : Controller
    {
        public string key = "b14ca5898a4e4133bbce2ea2315a1916";
        private readonly ILogger<UsersAdminController> _logger;
        private readonly TrainningContext _trainingContext;
        public UsersAdminController(ILogger<UsersAdminController> logger, TrainningContext context)
        {
            _logger = logger;
            _trainingContext = context;
        }
        public UserManage JoinUStbl(int x,string Ename)
        {
            var items = from c in _trainingContext.UserSignups
                        join d in _trainingContext.UserInfors
                        on c.EmployeeId equals d.UsId into ps
                        from d in ps.DefaultIfEmpty()
                        join f in _trainingContext.Departments
                        on c.DeptId equals f.DeptId into pg
                        from f in pg.DefaultIfEmpty()
                        select new SP_UserManage { EmployeeID = c.EmployeeId, EmployeeName = d.Sname + c.Fullname, Email = c.Email, PhoneNumber = d.PhoneNo, DeptID = c.DeptId, DeptName = f.Deptname };
            var itemsx = items.ToList(); 
            switch(x)
            {
                case 0:
                    itemsx = items.ToList();
                    break;
                case 1:
                    itemsx = items.Where(a => a.EmployeeName.ToLower().Contains(Ename)).ToList();
                    break;
            }    
             
            var model = new UserManage { Usermanage = itemsx, departments = _trainingContext.Departments.ToList() };
            return model;
        }
        [Route("UsersManage")]
        public IActionResult UserManage()
        {
            return View(JoinUStbl(0,""));
        }
        [HttpPost]
        [Route("UsersManage")]
        public IActionResult UserManage(string uname, string DepID,string email,string id)
        {
            PasswordHash Encrypt_password;
            Encrypt_password = new PasswordHash();
            UserSignup signup = new UserSignup()
            {
                EmployeeId = id,
                Fullname = uname,
                Email = email,
                DeptId = DepID,
                Password = Encrypt_password.Encrypt(key,"123456")
            };
            UserInfor userInfor = new UserInfor()
            {
                UsId = id
            };
            _trainingContext.UserSignups.Add(signup);
            _trainingContext.UserInfors.Add(userInfor);
            _trainingContext.SaveChanges();
            TempData["AdminSignup"] = "AdminSignup";
            return View(JoinUStbl(0, ""));
        }
        [Route("SearchUsers")]
        public PartialViewResult SearchUsers(string SearchText)
        {
            if(SearchText != null)
            {
                return PartialView("_PartialUserManage", JoinUStbl(1, SearchText));
            }    
            else
            {
                return PartialView("_PartialUserManage", JoinUStbl(0, ""));
            }    
        }
        [HttpPost]
        [Route("Delete_Row")]
        public async Task<IActionResult> Delete_Row(int id)
        {
            if (ModelState.IsValid)
            {
                var i = new Tranning_Apps_Management.Models.DB.UserSignup { EmployeeId = id.ToString() };
                var f = new Tranning_Apps_Management.Models.DB.UserInfor { UsId = id.ToString() };
                if (f != null)
                {
                    _trainingContext.UserInfors.Remove(f);
                }
                _trainingContext.UserSignups.Remove(i);
                await _trainingContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [Route("GroupManage")]
        public IActionResult GroupManage()
        {
            return View();
        }
        [Route("DepartmentManage")]
        public IActionResult DepartmentManage()
        {
            return View();
        }
    }
}
