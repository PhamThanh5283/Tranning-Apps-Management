using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tranning_Apps_Management.Models.DB;

namespace Tranning_Apps_Management.Controllers
{
    public class OrthersController : Controller
    {
        private readonly ILogger<OrthersController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        public OrthersController(ILogger<OrthersController> logger,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
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
        public IActionResult Contact()
        {
            Checkbackdrop();
            return View();
        }
    }
}
