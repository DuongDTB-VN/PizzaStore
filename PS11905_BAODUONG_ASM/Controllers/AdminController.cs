using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PS11905_BAODUONG_ASM.Services;
using PS11905_BAODUONG_ASM.Constant;
using PS11905_BAODUONG_ASM.Models;
using PS11905_BAODUONG_ASM.Models.ViewModels;
using Newtonsoft.Json;

namespace PS11905_BAODUONG_ASM.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        private readonly IWebHostEnvironment _webHostEnvironment;
        private INguoidungSvc _nguoidungSvc;

        public AdminController(IWebHostEnvironment webHostEnvironment, INguoidungSvc nguoidungSvc )
        {
            _webHostEnvironment = webHostEnvironment;
            _nguoidungSvc = nguoidungSvc;
        }
        public ActionResult Index()
        {
            return View();  
        }

        public ActionResult Login(string returnUrl)
        {
            string userName = HttpContext.Session.GetString(SessionKey.Nguoidung.UserName);
            if (userName != null && userName != "") //Khúc này đã có session
            {
                return RedirectToAction("Index", "Home");
            }

            #region Hiển thị Login
            ViewLogin login = new ViewLogin();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewLogin viewLogin)
        {
            if (ModelState.IsValid)
            {
                Nguoidung nguoidung = _nguoidungSvc.Login(viewLogin);
                if (nguoidung != null)
                {
                    HttpContext.Session.SetString(SessionKey.Nguoidung.UserName, nguoidung.UserName);
                    HttpContext.Session.SetString(SessionKey.Nguoidung.FullName, nguoidung.FullName);
                    HttpContext.Session.SetString(SessionKey.Nguoidung.NguoidungContext,
                        JsonConvert.SerializeObject(nguoidung));

                    return RedirectToAction(nameof(Index), "Admin");
                }
            }
            return View(viewLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.Nguoidung.UserName);
            HttpContext.Session.Remove(SessionKey.Nguoidung.FullName);
            HttpContext.Session.Remove(SessionKey.Nguoidung.NguoidungContext);

            return RedirectToAction(nameof(Index), "Admin");
        }
       
    }
}
