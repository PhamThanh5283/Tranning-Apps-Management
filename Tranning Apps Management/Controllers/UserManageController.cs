using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Tranning_Apps_Management.Models;
using Tranning_Apps_Management.Models.DB;


namespace Tranning_Apps_Management.Controllers
{
    public class UserManageController : Controller
    {
        private readonly ILogger<UserManageController> _logger;
        private readonly TrainningContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserManageController(ILogger<UserManageController> logger, TrainningContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = httpContextAccessor;
        }
        public IActionResult UserInf()
        {
            var multimodels = new MultimodelUSInf();
            var id = "";
            if (_contextAccessor.HttpContext != null)
            {
                if (ModelState.IsValid)
                {
                    id = _contextAccessor.HttpContext.Session.GetString("UsID");
                    ViewBag.us = _contextAccessor.HttpContext.Session.GetString("Usname");

                    ViewBag.ID = id;
                    var item = _context.UserSignups.Where(x => x.EmployeeId == id).FirstOrDefault();
                    var itemInf = _context.UserInfors.Where(x => x.UsId == id).FirstOrDefault();
                    if (item != null)
                    {
                        ViewBag.email = item.Email;
                        ViewBag.DepID = item.DeptId;
                        var dept = _context.Departments.Where(x => x.DeptId == item.DeptId).FirstOrDefault();
                        if (dept != null) { ViewBag.Depname = dept.Deptname; }
                    }
                    if (itemInf != null) 
                    {
                        ViewBag.sname = itemInf.Sname;
                        ViewBag.country = itemInf.Country;
                        ViewBag.mobile = itemInf.PhoneNo;
                        ViewBag.add1 = itemInf.Address1;
                        ViewBag.add2 = itemInf.Address2;
                    }
                    multimodels.departments = _context.Departments.ToList();
                    multimodels.userSignups = _context.UserSignups.ToList();
                    return View(multimodels);
                }
            }
            return RedirectToAction("Home", "Index"); 
        }
        [HttpPost]
        public IActionResult UserInf(string id,string sname,string phoneno,string add1,string add2,string email,string country,string deptid)
        {
            var multimodels = new MultimodelUSInf();
            multimodels.departments = _context.Departments.ToList();
            UserInfor userInfor = new UserInfor()
            {
                UsId = id,
                Sname = sname,
                PhoneNo = phoneno,
                Address1 = add1,
                Address2 = add2,
                Country = country
            };
            // update to UserSignup
            var f = new UserSignup { EmployeeId = id, Email = email,DeptId = deptid };
            _context.Attach(f);
            _context.Entry(f).Property(p => p.Email).IsModified = true;
            _context.Entry(f).Property(p => p.DeptId).IsModified = true;
            try
            {;
                _context.Add(userInfor);
                _context.SaveChanges();
            }
            catch (Exception e) 
            {
                if (e is DbUpdateException)
                {
                    var number = (int)e.InnerException.GetType().GetProperty("Number").GetValue(e.InnerException);
                    if (number != null && (number == 2627 || number == 2601))
                    {
                        _context.Update(userInfor);
                        _context.SaveChanges();
                    }    
                }
            }
            return View(multimodels);
        }

        public IActionResult Courses() 
        {
            return View();
        }
    }
}
