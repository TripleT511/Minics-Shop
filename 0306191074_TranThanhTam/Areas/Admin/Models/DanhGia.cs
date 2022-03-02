using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Areas.Admin.Models
{
    public class DanhGia
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tài khoản")]
        [Required]
        public int TaiKhoanId { get; set; }

        [DisplayName("Tài khoản")]
        public TaiKhoan TaiKhoan { get; set; }

        [DisplayName("Sản phẩm")]
        [Required]
        public int SanPhamId { get; set; }

        [DisplayName("Sản phẩm")]
        public SanPham SanPham { get; set; }

        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Nội dung không được bỏ trống"), MinLength(5, ErrorMessage = "Tối thiểu 5 kí tự")]
        public string NoiDung { get; set; }

        [DisplayName("Số sao đánh giá")]
        [Required(ErrorMessage = "Bắt buộc đánh giá")]
        public int SaoDanhGia { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

    }
}
