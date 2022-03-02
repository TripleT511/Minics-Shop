using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _0306191074_TranThanhTam.Areas.Admin.Models;
using _0306191074_TranThanhTam.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace _0306191074_TranThanhTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsController : Controller
    {
        private readonly _0306191074_TranThanhTamContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public SanPhamsController(_0306191074_TranThanhTamContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/SanPhams
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            var _0306191074_TranThanhTamContext = _context.SanPhams.Include(s => s.LoaiSanPham).OrderBy(a => a.Gia);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(String txtSearch)
        {
            if(txtSearch != null)
            {
                var sanpham = await _context.SanPhams.Include(s => s.LoaiSanPham).Where(l => l.TenSanPham.Contains(txtSearch) || l.LoaiSanPham.TenLoaiSanPham.Contains(txtSearch) || l.MoTa.Contains(txtSearch) || l.ThuongHieu.Contains(txtSearch) || l.MauSac.Contains(txtSearch) || l.Gia.ToString().Contains(txtSearch)).OrderBy(a => a.Gia).ToListAsync();

                return View(sanpham);
            }

            var _0306191074_TranThanhTamContext = _context.SanPhams.Include(s => s.LoaiSanPham).OrderBy(a => a.Gia);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }

        // GET: Admin/SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.LoaiSanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            ViewData["LoaiSanPhamId"] = new SelectList(_context.LoaiSanPhams, "Id", "TenLoaiSanPham");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenSanPham,MoTa,Gia,LoaiSanPhamId,HinhAnh,FileHinhAnh,MauSac,ThuongHieu,SoLuong,TrangThai")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();

                if (sanPham.FileHinhAnh != null)
                {
                    var fileAnh = sanPham.Id.ToString() + Path.GetExtension(sanPham.FileHinhAnh.FileName);
                    var upLoad = Path.Combine(_webHostEnvironment.WebRootPath, "images", "sanpham");
                    var filePath = Path.Combine(upLoad, fileAnh);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        sanPham.FileHinhAnh.CopyTo(fs);
                        fs.Flush();
                    }
                    sanPham.HinhAnh = fileAnh;
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiSanPhamId"] = new SelectList(_context.LoaiSanPhams, "Id", "TenLoaiSanPham", sanPham.LoaiSanPhamId);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["LoaiSanPhamId"] = new SelectList(_context.LoaiSanPhams, "Id", "TenLoaiSanPham", sanPham.LoaiSanPhamId);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenSanPham,MoTa,Gia,LoaiSanPhamId,HinhAnh,FileHinhAnh,MauSac,ThuongHieu,SoLuong,TrangThai")] SanPham sanPham)
        {
            if (id != sanPham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);

                    if (sanPham.FileHinhAnh != null)
                    {
                        var fileDelete = Path.Combine(_webHostEnvironment.WebRootPath, "images", "sanpham", sanPham.HinhAnh);
                        FileInfo file = new FileInfo(fileDelete);
                        file.Delete();

                        var fileAnh = sanPham.Id.ToString() + Path.GetExtension(sanPham.FileHinhAnh.FileName);
                        var upLoad = Path.Combine(_webHostEnvironment.WebRootPath, "images", "sanpham");
                        var filePath = Path.Combine(upLoad, fileAnh);

                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            sanPham.FileHinhAnh.CopyTo(fs);
                            fs.Flush();
                        }
                        sanPham.HinhAnh = fileAnh;
                        _context.Update(sanPham);
                        await _context.SaveChangesAsync();
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.Id))
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
            ViewData["LoaiSanPhamId"] = new SelectList(_context.LoaiSanPhams, "Id", "TenLoaiSanPham", sanPham.LoaiSanPhamId);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.LoaiSanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.Id == id);
        }
    }
}
