using _0306191074_TranThanhTam.Areas.Admin.Models;
using _0306191074_TranThanhTam.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly _0306191074_TranThanhTamContext _context;

        public HomeController(ILogger<HomeController> logger, _0306191074_TranThanhTamContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("Email,MatKhau")] TaiKhoan taiKhoan)
        {
            var data = _context.TaiKhoans.Where(s => s.Email.Equals(taiKhoan.Email) && s.MatKhau.Equals(taiKhoan.MatKhau)).ToList();
            if (data.Count > 0 && data[0].PhanQuyen == true && data[0].TrangThai == true)
            {
                HttpContext.Session.SetString("taiKhoanad", "true");
                HttpContext.Session.SetString("TenTaiKhoanAD", data[0].TenTaiKhoan);
                HttpContext.Session.SetString("Avatar", data[0].AnhDaiDien);
                HttpContext.Session.SetString("idTaiKhoanAD", data[0].Id.ToString());
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToShortTimeString());
                HttpContext.Session.SetString("tokenAD", System.Convert.ToBase64String(plainTextBytes));
                HttpContext.Session.SetInt32("RoleAD", data[0].PhanQuyen == true ? 0 : 1);
                return RedirectToAction("ThongKe", "Home");
            }
            else
            {
                ViewBag.Error = "Login failed";
                return View();
            }
        }

        public IActionResult ThongKe()
        {
            ViewBag.CountTK = _context.TaiKhoans.ToList().Count;
            ViewBag.CountSP = _context.SanPhams.ToList().Count;
            ViewBag.CountLoaiSP = _context.LoaiSanPhams.ToList().Count;
            ViewBag.CountDG = _context.DanhGias.ToList().Count;
            ViewBag.CountHD = _context.HoaDons.Where(hd => hd.NgayXuatHD.Month == DateTime.Now.Month).ToList().Count;
            ViewBag.CountDT = String.Format("{0:#,##0} ₫", _context.HoaDons.Where(h => h.TrangThai == 4 && h.NgayXuatHD.Month == DateTime.Now.Month).Sum(t => t.TongTien));
            
            ViewBag.DT1 = _context.HoaDons.Where(h => h.TrangThai == 4 && h.NgayXuatHD.Month == 1).Sum(t => t.TongTien);
            ViewBag.DT2 = _context.HoaDons.Where(h => h.TrangThai == 4 && h.NgayXuatHD.Month == 2).Sum(t => t.TongTien);
            ViewBag.DT3 = _context.HoaDons.Where(h => h.TrangThai == 4 && h.NgayXuatHD.Month == 3).Sum(t => t.TongTien);
            ViewBag.DT4 = _context.HoaDons.Where(h => h.TrangThai == 4 && h.NgayXuatHD.Month == 4).Sum(t => t.TongTien);
            ViewBag.DT5 = _context.HoaDons.Where(h => h.TrangThai == 4 && h.NgayXuatHD.Month == 5).Sum(t => t.TongTien);

            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham");


            return View();
        }

        [HttpPost]
        public string Logout()
        {
            HttpContext.Session.SetString("taiKhoanad", "false");

            RedirectToAction("Index", "Home");
            return "";
        }
    }
}
