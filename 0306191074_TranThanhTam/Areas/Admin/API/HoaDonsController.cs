using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _0306191074_TranThanhTam.Areas.Admin.Models;
using _0306191074_TranThanhTam.Data;

namespace _0306191074_TranThanhTam.Areas.Admin.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HoaDonsController : ControllerBase
    {
        private readonly _0306191074_TranThanhTamContext _context;

        public HoaDonsController(_0306191074_TranThanhTamContext context)
        {
            _context = context;
        }

        // GET: api/HoaDons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDon>>> GetHoaDons()
        {
            return await _context.HoaDons.OrderByDescending(a => a.NgayXuatHD).ToListAsync();
        }

        [HttpGet]
        public JsonResult ThongKe(DateTime start, DateTime end)
        {
            var output = "";
            var hoadon = _context.HoaDons.Where(hd => hd.TrangThai == 4 && hd.NgayXuatHD >= start && hd.NgayXuatHD <= end.AddDays(1)).Include(a => a.Account).Include(c => c.ChiTietHoaDons).ThenInclude(s => s.SanPham).OrderByDescending(a => a.TongTien).ToList();
            foreach (var item in hoadon)
            {
                var li = "";
                foreach (var cthd in item.ChiTietHoaDons)
                {
                    li += "<li class='text-wrap'>" + cthd.SanPham.TenSanPham + "<strong> x " + cthd.SoLuong + "</strong>.</li>";
                }

                output += "<tr>" +
                    "<th scope='row'>" + item.Id + "</th>" +
                    "<td>" + item.Account.HoTen + "</td>" +
                    "<td><ul>" + li +
                    "</ul></td>" +
                    "<td>" + item.DiaChiGH + "</td>" +
                    "<td>" + String.Format("{0:MM/dd/yyyy}", item.NgayXuatHD) + "</td>" +
                    "<td>" + String.Format("{0:#,##0} ₫", item.TongTien) + "</td>" +
                    "</tr>";
            }

            output += "<input type='hidden' id='tongDT' value='" + String.Format("{0:#,##0} ₫", hoadon.Sum(h => h.TongTien)) + "' />";

            return new JsonResult(output);
        }

        [HttpGet]
        public JsonResult ThongKeSanPham(int top)
        {
            if (top == 0) top = 5;
            var output = "";
            var cthd = _context.ChiTietHoaDons.Include(ct => ct.HoaDon).Include(a => a.SanPham).ThenInclude(b => b.LoaiSanPham).Where(a => a.HoaDon.TrangThai == 4 && a.HoaDon.NgayXuatHD.Month == DateTime.Now.Month).OrderBy(m => m.SanPhamId).ToList();
            int i = 1;

            for (int m = 0; m <= cthd.Count; m++)
            {
                for (int n = m + 1; n < cthd.Count; n++)
                {
                    if (cthd[m].SanPhamId == cthd[n].SanPhamId)
                    {
                        cthd[m].SoLuong += cthd[n].SoLuong;
                        cthd.RemoveAt(n);
                        --n;
                        continue;
                    }
                }
            }

            cthd = cthd.OrderByDescending(m => m.SoLuong).Take(top).ToList();

            foreach (var item in cthd)
            {
                var tongTien = item.SoLuong * item.SanPham.Gia;
                output += "<tr>" +
                    "<td class='text-wrap'>" + i + "</td>" +
                    "<td class='text-wrap'>" + item.SanPham.TenSanPham + "</td>" +
                    "<td>" + item.SanPham.LoaiSanPham.TenLoaiSanPham + "</td>" +
                    "<td>" + item.SoLuong + "</td>" +
                    "<td>" + String.Format("{0:#,##0} ₫", tongTien) + "</td>" +
                    "</tr>";
                ++i;
            }

            return new JsonResult(output);
        }

        [HttpGet]
        public JsonResult ThongKeTheoSP(int sanPhamId, DateTime start, DateTime end)
        {
            var output = "";

            var cthd = _context.ChiTietHoaDons.Include(ct => ct.HoaDon).Include(a => a.SanPham).ThenInclude(b => b.LoaiSanPham).Where(a => a.SanPhamId == sanPhamId).Where(b => b.HoaDon.TrangThai == 4 && b.HoaDon.NgayXuatHD >= start && b.HoaDon.NgayXuatHD <= end.AddDays(1)).FirstOrDefault();
            var tongSL = _context.ChiTietHoaDons.Include(h => h.HoaDon).Where(a => a.SanPhamId == sanPhamId && a.HoaDon.TrangThai == 4).Sum(b => b.SoLuong);
            if(cthd == null) return new JsonResult(output);

            var tongTien = tongSL * cthd.SanPham.Gia;
                output += "<tr>" +
                    "<td class='text-wrap'>" + cthd.SanPham.TenSanPham + "</td>" +
                    "<td>" + cthd.SanPham.LoaiSanPham.TenLoaiSanPham + "</td>" +
                    "<td>" + tongSL + "</td>" +
                    "<td>" + String.Format("{0:#,##0} ₫", tongTien) + "</td>" +
                    "</tr>";

            return new JsonResult(output);
        }

        [HttpGet]
        public JsonResult ThongKeChart()
        {
            var output = "";
            var tongHDHuy = _context.HoaDons.Where(hd => hd.TrangThai == 5).Count();
            var tongHDTC = _context.HoaDons.Where(hd => hd.TrangThai == 4).Count();
            var tongHDCho = _context.HoaDons.Where(hd => hd.TrangThai == 0).Count();

            output += "<input type='hidden' id='lstHuy' value='" + tongHDHuy + "' />" +
                "<input type='hidden' id='lstTC' value='" + tongHDTC + "' />" +
                "<input type='hidden' id='lstCho' value='" + tongHDCho + "' />";
            return new JsonResult(output);
        }

        [HttpGet]
        public JsonResult GetHoaDon(int id, int loai)
        {
            var output = "";
            var hoadon = _context.HoaDons.Where(hd => hd.TaiKhoanId == id && hd.TrangThai == loai).Include(a => a.Account).Include(c => c.ChiTietHoaDons).ThenInclude(s => s.SanPham).OrderByDescending(a => a.NgayXuatHD).ToList();
            foreach(var item in hoadon)
            {
                var trangThai = "";
                if(loai == 1)
                {
                    trangThai += "<input type='hidden' value='" + item.Id + "' id='idHoaDon' />" +
                        "<a class='btn btn-primary text-light' href='#'  id='xacNhan'>Xác nhận đơn hàng</a>" +
                        "<a class='btn btn-danger text-light' href='#' id='Huy'>Huỷ đơn hàng</a>";
                } else if(loai == 2)
                {
                    trangThai = "<span class='text-primary'>Đơn hàng đang chờ vận chuyển</span>";
                }
                else if (loai == 3)
                {
                    trangThai = "<input type='hidden' value='" + item.Id + "' id='idHoaDon2' />" +
                        "<span>Bạn đã nhận được hàng hay chưa ? nếu có vui lòng bấm vào nút xác nhận dưới đây</span>" +
                      "<a class='btn btn-success text-light' href='#' id='xacNhan2'>Xác nhận</a>";

                } else if(loai == 4)
                {
                    trangThai = "<span class='text-success'>Nhận hàng thành công</span>";
                } else
                {
                    trangThai = "<span class='text-danger'>Đơn hàng đã bị huỷ</span>";
                }
                var li = "";
                foreach (var cthd in item.ChiTietHoaDons)
                {
                    li += "<li class='text-wrap'>" + cthd.SanPham.TenSanPham + "<strong> x " + cthd.SoLuong + "</strong>.</li>";
                }

                output += "<tr>" +
                    "<th scope='row'>" + item.Id + "</th>" +
                    "<td>" + item.Account.HoTen + "</td>" +
                    "<td><ul>" + li +
                    "</ul></td>" +
                    "<td>" + item.DiaChiGH + "</td>" +
                    "<td>" + String.Format("{0:MM/dd/yyyy}", item.NgayXuatHD)  + "</td>" +
                    "<td>" + String.Format("{0:#,##0} ₫", item.TongTien) + "</td>" +
                    "<td>" + trangThai + "</td>" +
                    "</tr>"; 
            }

            return new JsonResult(output);
        }

        // GET: api/HoaDons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDon>> GetHoaDon(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return hoaDon;
        }

        // PUT: api/HoaDons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoaDon(int id, HoaDon hoaDon)
        {
            if (id != hoaDon.Id)
            {
                return BadRequest();
            }

            _context.Entry(hoaDon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HoaDons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HoaDon>> PostHoaDon(HoaDon hoaDon)
        {
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoaDon", new { id = hoaDon.Id }, hoaDon);
        }

        // DELETE: api/HoaDons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoaDon(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            _context.HoaDons.Remove(hoaDon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.Id == id);
        }
    }
}
