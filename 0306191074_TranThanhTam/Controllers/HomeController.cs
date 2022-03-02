using _0306191074_TranThanhTam.Areas.Admin.Models;
using _0306191074_TranThanhTam.Data;
using _0306191074_TranThanhTam.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly _0306191074_TranThanhTamContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public HomeController(ILogger<HomeController> logger, _0306191074_TranThanhTamContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var sanphamhot = await _context.SanPhams.Include(s => s.LoaiSanPham).Include(d => d.DanhGias).Where(s => s.TrangThai == true).OrderByDescending(a => a.Gia).Take(3).ToListAsync();
            var sanpham = await _context.SanPhams.Include(s => s.LoaiSanPham).Include(d => d.DanhGias).Where(s => s.LoaiSanPham.Id == 1).OrderByDescending(a => a.Id).Take(3).ToListAsync();
            var banphim = await _context.SanPhams.Include(s => s.LoaiSanPham).Include(d => d.DanhGias).Where(s => s.LoaiSanPham.Id == 2).OrderByDescending(a => a.Id).Take(3).ToListAsync();
            var manhinh = await _context.SanPhams.Include(s => s.LoaiSanPham).Include(d => d.DanhGias).Where(s => s.LoaiSanPham.Id == 3).OrderByDescending(a => a.Id).Take(3).ToListAsync();

            var lstDanhGia = await _context.DanhGias.Where(dg => dg.SaoDanhGia == 5).Where(a => a.TrangThai == true).OrderByDescending(t => t.Id).Include(tk => tk.TaiKhoan).Take(5).ToListAsync();
            var lstSlider = await _context.SlideShows.Where(sl => sl.TrangThai == true).Include(a => a.SanPham).OrderByDescending(t => t.NgayDang).Take(5).ToListAsync();
            ViewData["danhgia"] = lstDanhGia;
            ViewData["slide"] = lstSlider;
            ViewData["banphim"] = banphim;
            ViewData["manhinh"] = manhinh;
            ViewData["hot"] = sanphamhot;
            return View(sanpham);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("Email,MatKhau")] TaiKhoan taiKhoan)
        {

            var data = _context.TaiKhoans.Where(s => s.Email.Equals(taiKhoan.Email) && s.MatKhau.Equals(taiKhoan.MatKhau)).ToList();
            if (data.Count > 0 && data[0].TrangThai == true)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToShortTimeString());

                if (data[0].PhanQuyen == true)
                {
                    HttpContext.Session.SetString("taiKhoanad", "true");
                    HttpContext.Session.SetString("TenTaiKhoan", data[0].TenTaiKhoan);
                    HttpContext.Session.SetString("idTaiKhoan", data[0].Id.ToString());
                    HttpContext.Session.SetString("token", System.Convert.ToBase64String(plainTextBytes));
                    HttpContext.Session.SetInt32("Role", data[0].PhanQuyen == true ? 0 : 1);
                    return RedirectToAction("TaiKhoans", "Admin");
                }
                HttpContext.Session.SetString("taiKhoan", "true");
                HttpContext.Session.SetString("TenTaiKhoan", data[0].TenTaiKhoan);
                HttpContext.Session.SetString("idTaiKhoan", data[0].Id.ToString());
                HttpContext.Session.SetString("token", System.Convert.ToBase64String(plainTextBytes));
                HttpContext.Session.SetInt32("Role", data[0].PhanQuyen == true ? 0 : 1);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Login failed";
                return View();
            }
        }
        

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,TenTaiKhoan,MatKhau,Email,SoDienThoai,DiaChi,HoTen,FileHinhAnh,AnhDaiDien,TrangThai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(taiKhoan);
                //await _context.SaveChangesAsync();

                if (taiKhoan.FileHinhAnh != null)
                {
                    var fileAnh = taiKhoan.Id.ToString() + Path.GetExtension(taiKhoan.FileHinhAnh.FileName);
                    var upLoad = Path.Combine(_webHostEnvironment.WebRootPath, "images", "taikhoan");
                    var filePath = Path.Combine(upLoad, fileAnh);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        taiKhoan.FileHinhAnh.CopyTo(fs);
                        fs.Flush();
                    }
                    taiKhoan.AnhDaiDien = fileAnh;
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Login));
            }
            return View(taiKhoan);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Search(String tenSP, int? page, int? pageCount)
        {
            if(pageCount != null && page >= pageCount)
            {
                page = pageCount;
            }
            if (page == null) page = 1;

            var sanPham = await _context.SanPhams.Where(i => i.TenSanPham.Contains(tenSP) || i.LoaiSanPham.TenLoaiSanPham.Contains(tenSP) || i.Gia.ToString() == tenSP).Include(s => s.LoaiSanPham).Include(d => d.DanhGias).OrderByDescending(a => a.Gia).ToListAsync();

            int pageSize = 6;

            int pageNumber = (page ?? 1);

            ViewBag.PrevSearch = tenSP;
            ViewBag.PageNumber = pageNumber;

            return View(sanPham.ToPagedList(pageNumber, pageSize));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
