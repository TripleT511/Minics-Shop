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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace _0306191074_TranThanhTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideShowsController : Controller
    {
        private readonly _0306191074_TranThanhTamContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlideShowsController(_0306191074_TranThanhTamContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/SlideShows
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            var _0306191074_TranThanhTamContext = _context.SlideShows.Include(s => s.SanPham).OrderByDescending(a => a.NgayDang);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(String txtSearch, DateTime ngay)
        {
            if(txtSearch != null && ngay.Year != 0001)
            {
                var slide = _context.SlideShows.Include(s => s.SanPham).Where(a => a.NgayDang.Date == ngay.Date).Where(b => b.SanPham.TenSanPham.Contains(txtSearch) || b.TieuDe.Contains(txtSearch) || b.NoiDung.Contains(txtSearch)).OrderByDescending(a => a.NgayDang).ToListAsync();

                return View(await slide);
            }
            if(ngay.Year != 0001)
            {
                var slide = await _context.SlideShows.Include(s => s.SanPham).Where(b => b.NgayDang.Date == ngay.Date).OrderByDescending(a => a.NgayDang).ToListAsync();

                return View(slide);
            }
            if(txtSearch != null)
            {
                var slide = _context.SlideShows.Include(s => s.SanPham).Where(b => b.SanPham.TenSanPham.Contains(txtSearch) || b.TieuDe.Contains(txtSearch) || b.NoiDung.Contains(txtSearch)).OrderByDescending(a => a.NgayDang).ToListAsync();

                return View(await slide);
            }

            var _0306191074_TranThanhTamContext = _context.SlideShows.Include(s => s.SanPham).OrderByDescending(a => a.NgayDang);
            return View(await _0306191074_TranThanhTamContext.ToListAsync());
        }

        // GET: Admin/SlideShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var slideShow = await _context.SlideShows
                .Include(s => s.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slideShow == null)
            {
                return NotFound();
            }

            return View(slideShow);
        }

        // GET: Admin/SlideShows/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham");
            return View();
        }

        // POST: Admin/SlideShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SanPhamId,HinhAnh,FileHinhAnh,TieuDe,NoiDung,NgayDang,TrangThai")] SlideShow slideShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slideShow);
                await _context.SaveChangesAsync();
                if (slideShow.FileHinhAnh != null)
                {
                    var fileAnh = slideShow.Id.ToString() + Path.GetExtension(slideShow.FileHinhAnh.FileName);
                    var upLoad = Path.Combine(_webHostEnvironment.WebRootPath, "images", "slideshow");
                    var filePath = Path.Combine(upLoad, fileAnh);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        slideShow.FileHinhAnh.CopyTo(fs);
                        fs.Flush();
                    }
                    slideShow.HinhAnh = fileAnh;
                    _context.Update(slideShow);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "HinhAnh", slideShow.SanPhamId);
            return View(slideShow);
        }

        // GET: Admin/SlideShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var slideShow = await _context.SlideShows.FindAsync(id);
            if (slideShow == null)
            {
                return NotFound();
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "TenSanPham", slideShow.SanPhamId);
            return View(slideShow);
        }

        // POST: Admin/SlideShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SanPhamId,HinhAnh,FileHinhAnh,TieuDe,NoiDung,NgayDang,TrangThai")] SlideShow slideShow)
        {
            if (id != slideShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slideShow);
                    if (slideShow.FileHinhAnh != null)
                    {
                        var fileDelete = Path.Combine(_webHostEnvironment.WebRootPath, "images", "slideshow", slideShow.HinhAnh);
                        FileInfo file = new FileInfo(fileDelete);
                        file.Delete();

                        var fileAnh = slideShow.Id.ToString() + Path.GetExtension(slideShow.FileHinhAnh.FileName);
                        var upLoad = Path.Combine(_webHostEnvironment.WebRootPath, "images", "slideshow");
                        var filePath = Path.Combine(upLoad, fileAnh);

                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            slideShow.FileHinhAnh.CopyTo(fs);
                            fs.Flush();
                        }
                        slideShow.HinhAnh = fileAnh;
                        _context.Update(slideShow);
                        await _context.SaveChangesAsync();
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlideShowExists(slideShow.Id))
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
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "Id", "HinhAnh", slideShow.SanPhamId);
            return View(slideShow);
        }

        // GET: Admin/SlideShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var slideShow = await _context.SlideShows
                .Include(s => s.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slideShow == null)
            {
                return NotFound();
            }

            return View(slideShow);
        }

        // POST: Admin/SlideShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slideShow = await _context.SlideShows.FindAsync(id);
            _context.SlideShows.Remove(slideShow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideShowExists(int id)
        {
            return _context.SlideShows.Any(e => e.Id == id);
        }
    }
}
