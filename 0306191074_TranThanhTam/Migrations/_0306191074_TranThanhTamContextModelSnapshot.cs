// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _0306191074_TranThanhTam.Data;

namespace _0306191074_TranThanhTam.Migrations
{
    [DbContext(typeof(_0306191074_TranThanhTamContext))]
    partial class _0306191074_TranThanhTamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.ChiTietHoaDon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DonGia")
                        .HasColumnType("int");

                    b.Property<int>("HoaDonId")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HoaDonId");

                    b.HasIndex("SanPhamId");

                    b.ToTable("ChiTietHoaDons");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.DanhGia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<int>("SaoDanhGia")
                        .HasColumnType("int");

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SanPhamId");

                    b.HasIndex("TaiKhoanId");

                    b.ToTable("DanhGias");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.GioHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SanPhamId");

                    b.HasIndex("TaiKhoanId");

                    b.ToTable("GioHangs");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.HoaDon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChiGH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayXuatHD")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.Property<int>("TongTien")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaiKhoanId");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.LoaiSanPham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TenLoaiSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("LoaiSanPhams");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.SanPham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Gia")
                        .HasColumnType("int");

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoaiSanPhamId")
                        .HasColumnType("int");

                    b.Property<string>("MauSac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThuongHieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("LoaiSanPhamId");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.SlideShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayDang")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SanPhamId");

                    b.ToTable("SlideShows");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.TaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnhDaiDien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhanQuyen")
                        .HasColumnType("bit");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("TenTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.ChiTietHoaDon", b =>
                {
                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.HoaDon", "HoaDon")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.SanPham", "SanPham")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.DanhGia", b =>
                {
                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.SanPham", "SanPham")
                        .WithMany("DanhGias")
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("DanhGias")
                        .HasForeignKey("TaiKhoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.GioHang", b =>
                {
                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.SanPham", "SanPham")
                        .WithMany("GioHangs")
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.TaiKhoan", "Account")
                        .WithMany("GioHangs")
                        .HasForeignKey("TaiKhoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.HoaDon", b =>
                {
                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.TaiKhoan", "Account")
                        .WithMany("HoaDons")
                        .HasForeignKey("TaiKhoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.SanPham", b =>
                {
                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.LoaiSanPham", "LoaiSanPham")
                        .WithMany("SanPhams")
                        .HasForeignKey("LoaiSanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiSanPham");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.SlideShow", b =>
                {
                    b.HasOne("_0306191074_TranThanhTam.Areas.Admin.Models.SanPham", "SanPham")
                        .WithMany("SlideShows")
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.LoaiSanPham", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.SanPham", b =>
                {
                    b.Navigation("ChiTietHoaDons");

                    b.Navigation("DanhGias");

                    b.Navigation("GioHangs");

                    b.Navigation("SlideShows");
                });

            modelBuilder.Entity("_0306191074_TranThanhTam.Areas.Admin.Models.TaiKhoan", b =>
                {
                    b.Navigation("DanhGias");

                    b.Navigation("GioHangs");

                    b.Navigation("HoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
