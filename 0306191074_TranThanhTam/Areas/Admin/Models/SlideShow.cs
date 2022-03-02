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
    public class SlideShow
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Mã sản phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public int SanPhamId { get; set; }

        [DisplayName("Mã sản phẩm")]
        public SanPham SanPham { get; set; }

        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Bắt buộc chọn hình ảnh"), RegularExpression(@"^[\d\w-]+\.(png|PNG|jpg|JPG)$", ErrorMessage = "Định dạng jpg hoặc png")]
        public string HinhAnh { get; set; }

        [DisplayName("Hỉnh ảnh")]
        [NotMapped]
        public IFormFile FileHinhAnh { get; set; }

        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề không được bỏ trống"), MinLength(10, ErrorMessage = "Tối thiểu 10 kí tự"), MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string TieuDe { get; set; }

        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Nội dung không được bỏ trống"), MinLength(10, ErrorMessage = "Tối thiểu 10 kí tự"), MaxLength(150, ErrorMessage = "Tối đa 150 kí tự")]
        public string NoiDung { get; set; }

        [DisplayName("Ngày đăng")]
        [Required(ErrorMessage = "Cần phải chọn ngày đăng")]
        public DateTime NgayDang { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

    }
}
