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
    public class GioHangsController : Controller
    {
        private readonly _0306191074_TranThanhTamContext _context;

        public GioHangsController(_0306191074_TranThanhTamContext context)
        {
            _context = context;
        }

        // GET: Admin/GioHangs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            var _0306191074_TranThanhTamContext = _context.GioHangs.Include(g => g.Account).Include(g => g.SanPham).OrderByDescending(a => a.SoLuong);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }

        // GET: Admin/GioHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHangs
                .Include(g => g.Account)
                .Include(g => g.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // GET: Admin/GioHangs/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen");
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham");
            return View();
        }

        // POST: Admin/GioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaiKhoanId,SanPhamId,SoLuong")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gioHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", gioHang.TaiKhoanId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham", gioHang.SanPhamId);
            return View(gioHang);
        }

        // GET: Admin/GioHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHangs.FindAsync(id);
            if (gioHang == null)
            {
                return NotFound();
            }
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", gioHang.TaiKhoanId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham", gioHang.SanPhamId);
            return View(gioHang);
        }

        // POST: Admin/GioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaiKhoanId,SanPhamId,SoLuong")] GioHang gioHang)
        {
            if (id != gioHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gioHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioHangExists(gioHang.Id))
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
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", gioHang.TaiKhoanId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham", gioHang.SanPhamId);
            return View(gioHang);
        }

        // GET: Admin/GioHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHangs
                .Include(g => g.Account)
                .Include(g => g.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // POST: Admin/GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gioHang = await _context.GioHangs.FindAsync(id);
            _context.GioHangs.Remove(gioHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioHangExists(int id)
        {
            return _context.GioHangs.Any(e => e.Id == id);
        }
    }
}
