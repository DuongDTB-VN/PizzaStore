using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS11905_BAODUONG_ASM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PS11905_BAODUONG_ASM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using PS11905_BAODUONG_ASM.Constant;
using PS11905_BAODUONG_ASM.Models.ViewModels;
using Newtonsoft.Json;
using PS11905_BAODUONG_ASM.Filters;

namespace PS11905_BAODUONG_ASM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IWebHostEnvironment _webHostEnvironment;
        IMonAnSvc _monAnSvc;
        IKhachhangSvc _khachhangSvc;
        IDonhangSvc _donhangSvc;
        IDonhangChitietSvc _donhangChitietSvc;


        public HomeController(IWebHostEnvironment webHostEnvironment, IMonAnSvc monAnSvc, IKhachhangSvc khachhangSvc,
            IDonhangSvc donhangSvc, IDonhangChitietSvc donhangChitietSvc)
        {
            _webHostEnvironment = webHostEnvironment;
            _monAnSvc = monAnSvc;
            _khachhangSvc = khachhangSvc;
            _donhangSvc = donhangSvc;
            _donhangChitietSvc = donhangChitietSvc;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var getFoods = await _monAnSvc.GetFoodsAsync();
            return View(getFoods);
        }

        public IActionResult HistoryDetail(int id)
        {
            return View(_donhangSvc.GetDonhang(id));
        }

        public IActionResult AddCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null)
            {
                var monAn = _monAnSvc.GetMonAn(id);
                List<ViewCart> listCart = new List<ViewCart>()
                {
                    new ViewCart
                    {
                        MonAn = monAn,
                        Quantity = 1
                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    var monAn = _monAnSvc.GetMonAn(id);
                    dataCart.Add(new ViewCart
                    {
                        MonAn = monAn,
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }
            return Ok();
        }
        #region Login
        public IActionResult Login(string returnUrl)
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.Khachhang.KH_FullName);
            if (kH_Email != null && kH_Email != "")
            {
                return RedirectToAction("Index", "Home");
            }
            #region Hiển thị Login
            ViewWebLogin login = new ViewWebLogin();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewWebLogin viewWebLogin)
        {
            if (ModelState.IsValid)
            {
                Khachhang khachhang = _khachhangSvc.Login(viewWebLogin);
                if (khachhang != null)
                {
                    HttpContext.Session.SetString(SessionKey.Khachhang.KH_Email, khachhang.Email);
                    HttpContext.Session.SetString(SessionKey.Khachhang.KH_FullName, khachhang.FullName);
                    HttpContext.Session.SetString(SessionKey.Khachhang.KhachhangContext,
                        JsonConvert.SerializeObject(khachhang));

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View(viewWebLogin);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.Khachhang.KH_Email);
            HttpContext.Session.Remove(SessionKey.Khachhang.KH_FullName);
            HttpContext.Session.Remove(SessionKey.Khachhang.KhachhangContext);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidateEmail(string email)
        {
            if (_khachhangSvc.FindEmail(email)) return Ok();
            return StatusCode(404);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Khachhang khachhang)
        {
            try
            {
                _khachhangSvc.AddKhachhang(khachhang);
                return RedirectToAction(nameof(Login), new { id = khachhang.KhachhangID });
            }
            catch
            {

                return View();
            }
        }

        [AuthenticationFilterAttribute_KH]
        public ActionResult Info()
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.Khachhang.KH_FullName);

            if (kH_Email == null || kH_Email == "")
            {
                return RedirectToAction("Index", "Home");

            }

            var khachhangContext = HttpContext.Session.GetString(SessionKey.Khachhang.KhachhangContext);
            var khachhangId = JsonConvert.DeserializeObject<Khachhang>(khachhangContext).KhachhangID;
            var khachhang = _khachhangSvc.GetKhachhang(khachhangId);
            return View(khachhang);
        }

        [AuthenticationFilterAttribute_KH]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(int khachhangID, Khachhang khachhang)
        {
            try
            {
                // alo v thooi tu tu :v z dc roi 
                if (ModelState.IsValid)
                {
                    _khachhangSvc.EditKhachhang(khachhangID, khachhang);
                    return View();
                }
            }
            catch
            {


            }
            return RedirectToAction(nameof(Index));
        }

        //Phan Cart
        public IActionResult Cart() //GetCart
        {
            List<ViewCart> dataCart = new List<ViewCart>();
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
            }
            return View(dataCart);
        }

        [HttpPost]
        public IActionResult UpdateCart(int id, int soluong) //UpdateCart
        {
            var cart = HttpContext.Session.GetString("cart");
            double total = 0;
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart[i].Quantity = soluong;
                        break;
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));

                total = Tongtien();
                return Ok(total);
            }
            return BadRequest();
        }

        public IActionResult DeleteCart(int id) //Delete Cart
        {
            double total = 0;
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                total = Tongtien();
                return Ok(total);
            }
            return BadRequest();
        }

        public IActionResult OrderCart() //OrderCart
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.Khachhang.KH_FullName);
            if (kH_Email == null || kH_Email == "")  // đã có session
            {
                return BadRequest();
            }
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null && cart.Count() > 0)
            {
                #region DonHang
                var khachhangContext = HttpContext.Session.GetString(SessionKey.Khachhang.KhachhangContext);
                var khachhangId = JsonConvert.DeserializeObject<Khachhang>(khachhangContext).KhachhangID;

                double total = Tongtien();

                var donhang = new Donhang()
                {
                    Trangthai = TrangthaiDonhang.Moidat,
                    KhachhangID = khachhangId,
                    Tongtien = total,
                    Ngaydat = DateTime.Now,
                    Ghichu = "",
                    DiaChi = "HCM"
                };

                _donhangSvc.AddDonhang(donhang);
                int donhangId = donhang.DonhangID;

                #region Chitiet
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    DonhangChitiet chitiet = new DonhangChitiet()
                    {
                        DonhangID = donhangId,
                        MonAnID = dataCart[i].MonAn.MonAnID,
                        Soluong = dataCart[i].Quantity,
                        Thanhtien = dataCart[i].MonAn.Gia * dataCart[i].Quantity,
                        Ghichu = "",
                    };
                    //donhang.DonhangChitiets.Add(chitiet);
                    _donhangChitietSvc.AddDonhangChitietSvc(chitiet);
                }

                #endregion
                #endregion

                HttpContext.Session.Remove("cart");

                return Ok();
            }
            return BadRequest();
        }

        [NonAction]
        private double Tongtien() //TongTien
        {
            double total = 0;
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    total += (dataCart[i].MonAn.Gia * dataCart[i].Quantity);
                }
            }
            return total;
        }

        public async Task<ActionResult> Details(int id)
        {
            var food = await _monAnSvc.GetFoodById(id);
            if (food == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(food);
        }

        [AuthenticationFilterAttribute_KH]
        public async Task<IActionResult> History()
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.Khachhang.KH_Email); //=))
            if (kH_Email == null || kH_Email == "")  // đã có session
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await _donhangSvc.GetBillByCustomerEmail(kH_Email));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return View(await _monAnSvc.GetFoodsAsyncByName(searchString));
            }

            return View(await _monAnSvc.GetFoodsAsync());
        }

        //[HttpPost]
        //public async Task<IActionResult> SearchProduct(string name)
        //{
        //    try
        //    {
        //        var getMonan = await _monAnSvc.GetFoodsAsyncByName(name);
        //        if (getMonan.Count > 0)
        //        {
        //            return Json(new { success = true, Products = getMonan });
        //        }
        //        else return Json(new { success = true });
        //    }
        //    catch
        //    {
        //        return Json(new { success = false });
        //    }
        //}

    }
}
