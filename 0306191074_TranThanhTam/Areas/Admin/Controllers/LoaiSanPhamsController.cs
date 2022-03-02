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
    public class LoaiSanPhamsController : Controller
    {
        private readonly _0306191074_TranThanhTamContext _context;

        public LoaiSanPhamsController(_0306191074_TranThanhTamContext context)
        {
            _context = context;
        }

        // GET: Admin/LoaiSanPhams
        public async Task<IActionResult> Index(int? Id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            ViewBag.LstSanPham = await _context.LoaiSanPhams.Include(a => a.SanPhams).OrderByDescending(b => b.TenLoaiSanPham).ToListAsync();

            if (Id != null)
            {
                var loaisanpham = _context.LoaiSanPhams.Where(sp => sp.Id == Id).Include(a => a.SanPhams).FirstOrDefault();
                return View(loaisanpham);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? Id, string txtSearch)
        {
            if(txtSearch ==  null)
            {
                ViewBag.LstSanPham = await _context.LoaiSanPhams.Include(a => a.SanPhams).OrderByDescending(b => b.TenLoaiSanPham).ToListAsync();

                return View();
            }

            ViewBag.LstSanPham = await _context.LoaiSanPhams.Include(a => a.SanPhams).Where(a => a.TenLoaiSanPham.Contains(txtSearch)).OrderByDescending(b => b.TenLoaiSanPham).ToListAsync();

            if (Id != null)
            {
                var loaisanpham = _context.LoaiSanPhams.Where(sp => sp.Id == Id).Include(a => a.SanPhams).FirstOrDefault();
                return View(loaisanpham);
            }

            return View();
        }

        // GET: Admin/LoaiSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPhams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPhams/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            return View();
        }



        // POST: Admin/LoaiSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenLoaiSanPham,TrangThai")] LoaiSanPham loaiSanPham)
        {
            if(loaiSanPham.TenLoaiSanPham == null)
            {
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.LstSanPham = await _context.LoaiSanPhams.ToListAsync();
            
            loaiSanPham.TrangThai = true;
            _context.Add(loaiSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Admin/LoaiSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            return View("Index");
        }

        // POST: Admin/LoaiSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenLoaiSanPham,TrangThai")] LoaiSanPham loaiSanPham)
        {
            if (id != loaiSanPham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSanPhamExists(loaiSanPham.Id))
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
            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPhams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }

        // POST: Admin/LoaiSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSanPham = await _context.LoaiSanPhams.FindAsync(id);
            _context.LoaiSanPhams.Remove(loaiSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSanPhamExists(int id)
        {
            return _context.LoaiSanPhams.Any(e => e.Id == id);
        }
    }
}
