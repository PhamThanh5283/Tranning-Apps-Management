using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Tranning_Apps_Management.Models;
using Tranning_Apps_Management.Models.DB;
using Tranning_Apps_Management.Utils;

namespace Tranning_Apps_Management.Controllers
{
    public class HomeController : Controller
    {
        public string key = "b14ca5898a4e4133bbce2ea2315a1916";
        private readonly ILogger<HomeController> _logger;
        private readonly TrainningContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        //private static string GlobalUsname = "";
        public HomeController(ILogger<HomeController> logger, TrainningContext context,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = httpContextAccessor;
        }
        private void Checkbackdrop()
        {
            var SessionUS = _contextAccessor.HttpContext.Session.GetString("Usname");
            if (SessionUS != null)
            {
                ViewBag.us = SessionUS;
                ViewBag.toggle = "";
                ViewBag.target = "";
            }
            else
            {
                ViewBag.toggle = "modal";
                ViewBag.target = "#staticBackdrop";
            }
        }
        public IActionResult Index()
        {
            Checkbackdrop();
            return View();
        }
        public IActionResult Signup()
        {
            var items = _context.Departments.ToList();
            return View(items);
        }
        [HttpPost]
        public async Task<IActionResult> AddSignup(AddSignup addSignup)
        {
            PasswordHash Encrypt_password;
            Encrypt_password = new PasswordHash();
            UserSignup userSignup = new UserSignup()
            {
                EmployeeId = addSignup.EmployeeId,
                Fullname = addSignup.Fullname,
                Email = addSignup.Email,
                DeptId = addSignup.DepID,
                Password = Encrypt_password.Encrypt(key, addSignup.Password)
            };
            UserInfor userInfor = new UserInfor()
            {
                UsId = addSignup.EmployeeId
            };
            _context.Add(userSignup);
            _context.Add(userInfor);
            TempData["signup"] = "Pass";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(string uname,string pswd)
        {
            PasswordHash Encrypt_password;
            Encrypt_password = new PasswordHash();
            string checkpw = Encrypt_password.Encrypt(key, pswd);
            var item = await _context.UserSignups.Where(x => x.EmployeeId == uname && x.Password == checkpw).FirstOrDefaultAsync();
            if (item != null) 
            {
                //GlobalUsname = item.Fullname;
                if (item.Fullname !=null && _contextAccessor.HttpContext != null)
                {
                   _contextAccessor.HttpContext.Session.SetString("Usname", item.Fullname);
                    _contextAccessor.HttpContext.Session.SetString("UsID", uname);
                    TempData["login"] = "Success";
                    TempData["UsName"] = item.Fullname;
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["login"] = "Fail";
                return RedirectToAction("Index", "Home");
            }     
        }
        public IActionResult Logout()
        {
            //GlobalUsname = "";
            if (_contextAccessor.HttpContext != null) 
            {
                if (_contextAccessor.HttpContext.Session.GetString("Usname") != null)
                {
                    HttpContext.Session.Clear();
                    TempData["logout"] = "True";
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Forgot()
        {
            Checkbackdrop();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}