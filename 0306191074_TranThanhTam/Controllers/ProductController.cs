using _0306191074_TranThanhTam.Areas.Admin.Models;
using _0306191074_TranThanhTam.Data;
using _0306191074_TranThanhTam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly _0306191074_TranThanhTamContext _context;

        public ProductController(ILogger<ProductController> logger, _0306191074_TranThanhTamContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(int? page, int? pageCount)
        {
            if (pageCount != null && page >= pageCount)
            {
                page = pageCount;
            }
            if (page == null) page = 1;

            var sanPham = await _context.SanPhams.Include(s => s.LoaiSanPham).Include(d => d.DanhGias).OrderByDescending(a => a.Gia).ToListAsync();

            int pageSize = 6;

            int pageNumber = (page ?? 1);

            ViewBag.PageNumber = pageNumber;
            ViewBag.Loai = 0;
            return View(sanPham.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(int Loai, int? page, int? pageCount)
        {
            if(Loai == 0)
            {
                if (pageCount != null && page >= pageCount)
                {
                    page = pageCount;
                }
                if (page == null) page = 1;

                var sanPham = await _context.SanPhams.Include(s => s.LoaiSanPham).Include(d => d.DanhGias).OrderByDescending(a => a.Gia).ToListAsync();

                int pageSize = 6;

                int pageNumber = (page ?? 1);

                ViewBag.PageNumber = pageNumber;
                ViewBag.Loai = 0;
                return View(sanPham.ToPagedList(pageNumber, pageSize));
            } else
            {
                if (pageCount != null && page >= pageCount)
                {
                    page = pageCount;
                }
                if (page == null) page = 1;

                var sanPham = await _context.SanPhams.Where(l => l.LoaiSanPham.Id == Loai).Include(s => s.LoaiSanPham).Include(d => d.DanhGias).OrderByDescending(a => a.Gia).ToListAsync();

                int pageSize = 6;

                int pageNumber = (page ?? 1);

                ViewBag.PageNumber = pageNumber;
                ViewBag.Loai = Loai;
                return View(sanPham.ToPagedList(pageNumber, pageSize));
            }
        }

        public async Task<IActionResult> DetailAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.LoaiSanPham)
                .Include(dg => dg.DanhGias)
                    .ThenInclude(tk => tk.TaiKhoan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        [HttpPost]
        public string ThemGioHang(int TaiKhoanId, int SanPhamId, int SoLuong)
        {
            var giohang = _context.GioHangs.Where(gh => gh.SanPhamId == SanPhamId).ToList<GioHang>();
            if(giohang.Count == 0)
            {
                GioHang newGH = new GioHang();
                newGH.SanPhamId = SanPhamId;
                newGH.TaiKhoanId = TaiKhoanId;
                newGH.SoLuong = SoLuong;
                _context.Add(newGH);
                _context.SaveChanges();
            } else
            {
                giohang[0].SoLuong += SoLuong;
                _context.Update(giohang[0]);
                _context.SaveChanges();
                
            }
            return "Thêm Thành công";
        }

        

        [HttpPost]
        public string ThemDanhGia(int TaiKhoanId, int SanPhamId, string NoiDung,int SaoDanhGia)
        {

            DanhGia dg = new DanhGia();
            dg.TaiKhoanId = TaiKhoanId;
            dg.SanPhamId = SanPhamId;
            dg.NoiDung = NoiDung;
            dg.SaoDanhGia = SaoDanhGia;
            dg.TrangThai = true;
            _context.Add(dg);
            _context.SaveChanges();

            return "Thêm Thành công";
        }

        [HttpPost]
        public string XoaDanhGia(int id)
        {
            var danhGia = _context.DanhGias.Where(a => a.Id == id).FirstOrDefault();
            _context.Remove(danhGia);
            _context.SaveChanges();

            return "Đã xoá";
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
