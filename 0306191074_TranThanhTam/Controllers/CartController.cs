using _0306191074_TranThanhTam.Areas.Admin.Models;
using _0306191074_TranThanhTam.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        private readonly _0306191074_TranThanhTamContext _context;

        public CartController(ILogger<CartController> logger, _0306191074_TranThanhTamContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("taiKhoan") != "true")
                return RedirectToAction("Login", "Home");
            int idTK = Int32.Parse(HttpContext.Session.GetString("idTaiKhoan"));
            var gioHang =  await _context.GioHangs.Include(s => s.SanPham).Include(a => a.Account).Where(gh => gh.TaiKhoanId == idTK).ToListAsync();
            return View(gioHang);
        }

        [HttpPost]
        public string ThemSoLuong(int idGioHang, int max)
        {
            var giohang = _context.GioHangs.Where(gh => gh.Id == idGioHang).FirstOrDefault();
            if (giohang.SoLuong >= max) return "Đã vượt quá số lượng hiện có";
            giohang.SoLuong += 1;
            _context.Update(giohang);
            _context.SaveChanges();

            return "";
        }

        [HttpPost]
        public string GiamSoLuong(int idGioHang)
        {
            var giohang = _context.GioHangs.Where(gh => gh.Id == idGioHang).ToList();
            if(giohang[0].SoLuong <= 1)
            {
                _context.GioHangs.Remove(giohang[0]);
                _context.SaveChanges();

                return "Đã xoá sản phẩm khỏi giỏ hàng";
            } 
            giohang[0].SoLuong -= 1;
            _context.Update(giohang[0]);
            _context.SaveChanges();

            return "Cập nhật Thành công";
        }

        [HttpPost]
        public string Xoa(int id)
        {
            var giohang = _context.GioHangs.Where(i => i.SanPhamId == id).FirstOrDefault();

            _context.GioHangs.Remove(giohang);
            _context.SaveChanges();

            return "Xoá Thành công";
        }

        [HttpPost]
        public string XoaGioHang()
        {
            var giohang = _context.GioHangs.ToList();
            
            _context.GioHangs.RemoveRange(giohang);
            _context.SaveChanges();


            return "Xoá Thành công";
        }

        [HttpPost]
        public JsonResult ThanhToan(string values, int tongTien)
        {
            int idTK = Int32.Parse(HttpContext.Session.GetString("idTaiKhoan"));
            var taikhoan = _context.TaiKhoans.Where(tk => tk.Id == idTK).FirstOrDefault();
            HoaDon hd = new HoaDon();
            hd.TaiKhoanId = idTK;
            hd.NgayXuatHD = DateTime.Now;
            hd.DiaChiGH = taikhoan.DiaChi;
            hd.SoDienThoai = taikhoan.SoDienThoai;
            hd.TongTien = tongTien;
            hd.TrangThai = 0;
            _context.Add(hd);
            _context.SaveChanges();

            JArray jsonArray = JArray.Parse(values);
            for(var i = 0;i < jsonArray.ToArray().Length; i++)
            {
               var a = jsonArray.ToArray()[i];
                JToken id = a["id"];
                JToken soLuong = a["soluong"];
                JToken gia = a["dongia"];

                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.HoaDonId = hd.Id;
                cthd.SanPhamId = (int)id;
                cthd.SoLuong = (int)soLuong;
                cthd.DonGia = (int)gia;
                _context.Add(cthd);
                _context.SaveChanges();
            }

            var giohang = _context.GioHangs.ToList();

            _context.GioHangs.RemoveRange(giohang);
            _context.SaveChanges();

            return new JsonResult("Đặt hàng thành công ! Vui lòng đợi chúng tôi xác nhận thông tin");
        }




    }
}

