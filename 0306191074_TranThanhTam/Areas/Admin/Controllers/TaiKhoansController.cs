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
using System.IO;
using Microsoft.AspNetCore.Http;

namespace _0306191074_TranThanhTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiKhoansController : Controller
    {
        private readonly _0306191074_TranThanhTamContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public TaiKhoansController(_0306191074_TranThanhTamContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/TaiKhoans
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            return View(await _context.TaiKhoans.OrderBy(a => a.HoTen).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(String txtSearch)
        {
            if(txtSearch != null)
            {
                var lstTaiKhoan = await _context.TaiKhoans.Where(t => t.TenTaiKhoan.Contains(txtSearch) || t.HoTen.Contains(txtSearch) || t.DiaChi.Contains(txtSearch) || t.SoDienThoai.Contains(txtSearch)).OrderBy(a => a.HoTen).ToListAsync();

                return View(lstTaiKhoan);
            }

            return View(await _context.TaiKhoans.OrderBy(a => a.HoTen).ToListAsync());
        }

        // GET: Admin/TaiKhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenTaiKhoan,MatKhau,Email,SoDienThoai,DiaChi,HoTen,PhanQuyen,AnhDaiDien,FileHinhAnh,TrangThai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();

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

                return RedirectToAction(nameof(Index));
            }
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenTaiKhoan,MatKhau,Email,SoDienThoai,DiaChi,HoTen,PhanQuyen,AnhDaiDien,FileHinhAnh,TrangThai")] TaiKhoan taiKhoan)
        {

            if (id != taiKhoan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);

                    if (taiKhoan.FileHinhAnh != null)
                    {
                        var fileDelete = Path.Combine(_webHostEnvironment.WebRootPath, "images", "taikhoan", taiKhoan.AnhDaiDien);
                        FileInfo file = new FileInfo(fileDelete);
                        file.Delete();

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

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.Id))
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
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("taiKhoanad") != "true")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoans.Any(e => e.Id == id);
        }
    }
}
