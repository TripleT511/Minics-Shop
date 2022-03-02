using _0306191074_TranThanhTam.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Controllers
{
    public class UserController : Controller
    {
        private readonly _0306191074_TranThanhTamContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(_0306191074_TranThanhTamContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("taiKhoan") != "true")
                return RedirectToAction("Login", "Home");
            string id = HttpContext.Session.GetString("idTaiKhoan");
            var user = _context.TaiKhoans.Where(us => us.Id == Int32.Parse(id)).FirstOrDefault();
            var hoadon = _context.HoaDons.Where(hd => hd.TaiKhoanId == Int32.Parse(id) && hd.TrangThai != 0).Include(ct => ct.ChiTietHoaDons).ThenInclude(sp => sp.SanPham).OrderByDescending(a => a.NgayXuatHD).ToList();
            ViewData["lichsu"] = hoadon;
            return View(user);
        }

        [HttpPost]
        public string Logout()
        {
            HttpContext.Session.SetString("taiKhoan", "false");

            RedirectToAction("Home");
            return "";
        }

        [HttpPost]
        public string CapNhatTT(String HoTen, String DiaChi, String SoDienThoai)
        {
            if(HoTen != null && DiaChi != null && SoDienThoai != null)
            {
                string id = HttpContext.Session.GetString("idTaiKhoan");

                var user = _context.TaiKhoans.Where(us => us.Id == Int32.Parse(id)).FirstOrDefault();
                user.HoTen = HoTen;
                user.DiaChi = DiaChi;
                user.SoDienThoai = SoDienThoai;
                _context.Update(user);
                _context.SaveChanges();
                HttpContext.Session.SetString("TenTaiKhoan", HoTen);
                return "Cập nhật thành công";
            }
            
            return "Cập nhật thất bại";
        }

        [HttpPost]
        public string capNhatAnh(IFormFile FileHinhAnh)
        {
            if (FileHinhAnh != null)
            {
                string id = HttpContext.Session.GetString("idTaiKhoan");
                var user = _context.TaiKhoans.Where(us => us.Id == Int32.Parse(id)).FirstOrDefault();

                var fileDelete = Path.Combine(_webHostEnvironment.WebRootPath, "images", "taikhoan", user.AnhDaiDien);
                FileInfo file = new FileInfo(fileDelete);
                file.Delete();

                var fileAnh = user.Id.ToString() + Path.GetExtension(FileHinhAnh.FileName);
                var upLoad = Path.Combine(_webHostEnvironment.WebRootPath, "images", "taikhoan");
                var filePath = Path.Combine(upLoad, fileAnh);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    FileHinhAnh.CopyTo(fs);
                    fs.Flush();
                }
                user.AnhDaiDien = fileAnh;
                _context.Update(user);
                _context.SaveChanges();
            }

            return "Cập nhật ảnh đại diện thành công";
        }

        [HttpPost]
        public string XacNhan(int id)
        {
            var hoadon = _context.HoaDons.Where(hd => hd.Id == id).FirstOrDefault();
            hoadon.TrangThai = 2;
            _context.Update(hoadon);
            _context.SaveChanges();
            return "Đã xác nhận đơn hàng";
        }

        [HttpPost]
        public string XacNhan2(int id)
        {
            var hoadon = _context.HoaDons.Where(hd => hd.Id == id).FirstOrDefault();
            var cthd = _context.ChiTietHoaDons.Where(ct => ct.HoaDonId == hoadon.Id).ToList();
            foreach(var item in cthd)
            {
                var sanpham = _context.SanPhams.Where(sp => sp.Id == item.SanPhamId).FirstOrDefault();
                sanpham.SoLuong -= item.SoLuong;
                _context.Update(sanpham);
                _context.SaveChanges();
            }
            hoadon.TrangThai = 4;
            _context.Update(hoadon);
            _context.SaveChanges();
            return "Cảm ơn bạn đã mua sản phẩm của chúng tôi";
        }

        [HttpPost]
        public string Huy(int id)
        {
            var hoadon = _context.HoaDons.Where(hd => hd.Id == id).FirstOrDefault();
            hoadon.TrangThai = 5;
            _context.Update(hoadon);
            _context.SaveChanges();
            return "Đã huỷ đơn hàng";
        }


    }
}
