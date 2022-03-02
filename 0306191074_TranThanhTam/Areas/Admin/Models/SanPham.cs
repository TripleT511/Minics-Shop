using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Areas.Admin.Models
{
    public class SanPham
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống"), MinLength(5, ErrorMessage = "Tối thiểu 5 kí tự")]
        public string TenSanPham { get; set; }

        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Mô tả không được để trống"), MinLength(10, ErrorMessage = "Tối thiểu 10 kí tự")]
        public string MoTa { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} ₫")]
        [DisplayName("Giá")]
        [Required(ErrorMessage = "Giá không được để trống")]
        public int Gia { get; set; }

        [DisplayName("Loại sản phẩm")]
        [Required]
        public int LoaiSanPhamId { get; set; }

        [DisplayName("Loại sản phẩm")]
        public LoaiSanPham LoaiSanPham { get; set; }

        [DisplayName("Hỉnh ảnh")]
        [Required(ErrorMessage = "Bắt buộc chọn hình ảnh"), RegularExpression(@"^[\d\w-]+\.(png|PNG|jpg|JPG)$", ErrorMessage = "Định dạng jpg hoặc png")]
        public string HinhAnh { get; set; }

        [DisplayName("Hỉnh ảnh")]
        [NotMapped]
        public IFormFile FileHinhAnh { get; set; }

        [DisplayName("Màu sắc")]
        [Required(ErrorMessage = "Bắt buộc chọn màu sắc")]
        public string MauSac { get; set; }

        [DisplayName("Thương hiệu")]
        [Required(ErrorMessage = "Bắt buộc nhập thương hiệu")]
        public string ThuongHieu { get; set; }

        [DisplayName("Số lượng")]
        [Required(ErrorMessage = "Bắt buộc nhập số lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        public List<GioHang> GioHangs { get; set; }  

        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public List<DanhGia> DanhGias { get; set; }

        public List<SlideShow> SlideShows { get; set; }

    }
}
