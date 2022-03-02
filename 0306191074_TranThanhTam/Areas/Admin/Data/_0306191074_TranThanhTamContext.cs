using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _0306191074_TranThanhTam.Areas.Admin.Models;

namespace _0306191074_TranThanhTam.Data
{
    public class _0306191074_TranThanhTamContext : DbContext
    {
        public _0306191074_TranThanhTamContext (DbContextOptions<_0306191074_TranThanhTamContext> options)
            : base(options)
        {
        }

        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

        public DbSet<SanPham> SanPhams { get; set; }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }

        public DbSet<GioHang> GioHangs { get; set; }

        public DbSet<HoaDon> HoaDons { get; set; }

        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public DbSet<DanhGia> DanhGias { get; set; }

        public DbSet<SlideShow> SlideShows { get; set; }
    }
}
