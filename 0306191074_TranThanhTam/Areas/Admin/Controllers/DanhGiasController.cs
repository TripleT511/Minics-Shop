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
    public class DanhGiasController : Controller
    {
        private readonly _0306191074_TranThanhTamContext _context;

        public DanhGiasController(_0306191074_TranThanhTamContext context)
        {
            _context = context;
        }

        // GET: Admin/DanhGias
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            var _0306191074_TranThanhTamContext = _context.DanhGias.Include(d => d.SanPham).Include(d => d.TaiKhoan).OrderByDescending(a => a.SaoDanhGia);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Index(String txtSearch, int loai)
        {
            if(txtSearch != null)
            {
                var danhgia =  _context.DanhGias.Include(d => d.SanPham).Include(d => d.TaiKhoan).Where(b => b.NoiDung.Contains(txtSearch) || b.SanPham.TenSanPham.Contains(txtSearch) || b.TaiKhoan.TenTaiKhoan.Contains(txtSearch) || b.TaiKhoan.HoTen.Contains(txtSearch)).OrderByDescending(a => a.SaoDanhGia);
                if(loai != 0)
                {
                    return View(await danhgia.Where(a => a.SaoDanhGia == loai).ToListAsync());
                }
                return View(await danhgia.ToListAsync());
            }

            if(txtSearch == null && loai != 0)
            {
                var danhgia = _context.DanhGias.Include(d => d.SanPham).Include(d => d.TaiKhoan).Where(b => b.SaoDanhGia == loai).OrderByDescending(a => a.SanPhamId);
               
                return View(await danhgia.ToListAsync());
            }

            var _0306191074_TranThanhTamContext = _context.DanhGias.Include(d => d.SanPham).Include(d => d.TaiKhoan).OrderByDescending(a => a.SaoDanhGia);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }

        public string XacNhan(int id)
        {
            var danhgia = _context.DanhGias.Where(hd => hd.Id == id).FirstOrDefault();
            danhgia.TrangThai = true;
            _context.Update(danhgia);
            _context.SaveChanges();
            return "Đã duyệt đánh giá";
        }

        // GET: Admin/DanhGias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DanhGias
                .Include(d => d.SanPham)
                .Include(d => d.TaiKhoan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return View(danhGia);
        }

        // GET: Admin/DanhGias/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham");
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen");
            return View();
        }

        // POST: Admin/DanhGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaiKhoanId,SanPhamId,NoiDung,SaoDanhGia,TrangThai")] DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham", danhGia.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", danhGia.TaiKhoanId);
            return View(danhGia);
        }

        // GET: Admin/DanhGias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DanhGias.FindAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham", danhGia.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", danhGia.TaiKhoanId);
            return View(danhGia);
        }

        // POST: Admin/DanhGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaiKhoanId,SanPhamId,NoiDung,SaoDanhGia,TrangThai")] DanhGia danhGia)
        {
            if (id != danhGia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhGiaExists(danhGia.Id))
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
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham", danhGia.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "Id", "HoTen", danhGia.TaiKhoanId);
            return View(danhGia);
        }

        // GET: Admin/DanhGias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DanhGias
                .Include(d => d.SanPham)
                .Include(d => d.TaiKhoan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return View(danhGia);
        }

        // POST: Admin/DanhGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhGia = await _context.DanhGias.FindAsync(id);
            _context.DanhGias.Remove(danhGia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhGiaExists(int id)
        {
            return _context.DanhGias.Any(e => e.Id == id);
        }
    }
}
