using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PS11905_BAODUONG_ASM.Services;
using PS11905_BAODUONG_ASM.Models;

namespace PS11905_BAODUONG_ASM.Controllers
{
    public class NguoiDungController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private INguoidungSvc _nguoidungSvc;

        public NguoiDungController(IWebHostEnvironment webHostEnvironment, INguoidungSvc nguoidungSvc)
        {
            _webHostEnvironment = webHostEnvironment;
            _nguoidungSvc = nguoidungSvc;
        }

        // GET: NguoiDungController
        public ActionResult Index()
        {
            return View(_nguoidungSvc.GetNguoidungAll());
        }

        // GET: NguoiDungController/Details/5
        public ActionResult Details(int id)
        {
            var nguoidung = _nguoidungSvc.GetNguoidung(id);
            return View(nguoidung);
        }

        // GET: NguoiDungController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDungController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nguoidung nguoidung)
        {
            try
            {
                _nguoidungSvc.AddNguoidung(nguoidung);
                return RedirectToAction(nameof(Details), new { id = nguoidung.NguoidungID });
            }
            catch
            {
                return View();
            }
        }

        // GET: NguoiDungController/Edit/5
        public ActionResult Edit(int id)
        {
            var nguoidung = _nguoidungSvc.GetNguoidung(id);
            return View(nguoidung);
        }

        // POST: NguoiDungController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Nguoidung nguoidung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _nguoidungSvc.EditNguoidung(id, nguoidung);
                    return RedirectToAction(nameof(Details), new { id = nguoidung.NguoidungID });
                }

            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: NguoiDungController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NguoiDungController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
