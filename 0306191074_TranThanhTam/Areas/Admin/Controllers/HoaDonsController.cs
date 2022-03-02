using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _0306191074_TranThanhTam.Areas.Admin.Models;
using _0306191074_TranThanhTam.Data;
using Microsoft.AspNetCore.Http;

namespace _0306191074_TranThanhTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonsController : Controller
    {
        private readonly _0306191074_TranThanhTamContext _context;

        public HoaDonsController(_0306191074_TranThanhTamContext context)
        {
            _context = context;
        }

        // GET: Admin/HoaDons
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            var _0306191074_TranThanhTamContext = _context.HoaDons.Include(h => h.Account).Include(a => a.ChiTietHoaDons).ThenInclude(b => b.SanPham).OrderByDescending(a => a.NgayXuatHD);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int loai, DateTime ngayXuatHD)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");
            if(loai == 6 && ngayXuatHD.Year != 001)
            {
                var hoadon = _context.HoaDons.Include(h => h.Account).Where(b => b.NgayXuatHD.Date == ngayXuatHD.Date).Include(a => a.ChiTietHoaDons).ThenInclude(b => b.SanPham).OrderByDescending(a => a.NgayXuatHD); ;
                return View(await hoadon.ToListAsync());
            } else if(loai != 6 && ngayXuatHD.Year != 001)
            {
                var hoadon = _context.HoaDons.Include(h => h.Account).Where(b => b.NgayXuatHD.Date == ngayXuatHD.Date && b.TrangThai == loai).Include(a => a.ChiTietHoaDons).ThenInclude(b => b.SanPham).OrderByDescending(a => a.NgayXuatHD); ;
                return View(await hoadon.ToListAsync());
            }
            
            var _0306191074_TranThanhTamContext = _context.HoaDons.Where(a => a.TrangThai == loai).Include(h => h.Account).Include(a => a.ChiTietHoaDons).ThenInclude(b => b.SanPham).OrderByDescending(a => a.NgayXuatHD);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }

        public string XacNhan(int id)
        {
            var hoadon = _context.HoaDons.Where(hd => hd.Id == id).FirstOrDefault();
            hoadon.TrangThai = 1;
            _context.Update(hoadon);
            _context.SaveChanges();
            return "Đã xác nhận đơn hàng";
        }

        public string BanGiao(int id)
        {
            var hoadon = _context.HoaDons.Where(hd => hd.Id == id).FirstOrDefault();
            hoadon.TrangThai = 3;
            _context.Update(hoadon);
            _context.SaveChanges();
            return "Đã bàn giao cho đơn vị vận chuyển";
        }

        // GET: Admin/HoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaiKhoanId,NgayXuatHD,DiaChiGH,SoDienThoai,TongTien,Status")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", hoaDon.TaiKhoanId);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", hoaDon.TaiKhoanId);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaiKhoanId,NgayXuatHD,DiaChiGH,SoDienThoai,TongTien,Status")] HoaDon hoaDon)
        {
            if (id != hoaDon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", hoaDon.TaiKhoanId);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            _context.HoaDons.Remove(hoaDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.Id == id);
        }
    }
}
