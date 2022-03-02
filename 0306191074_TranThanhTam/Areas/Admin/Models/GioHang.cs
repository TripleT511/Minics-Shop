using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Areas.Admin.Models
{
    public class GioHang
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tài khoản")]
        [Required]
        public int TaiKhoanId { get; set; }

        [DisplayName("Tài khoản")]
        public TaiKhoan Account { get; set; }

        [DisplayName("Sản phẩm")]
        [Required]
        public int SanPhamId { get; set; }

        [DisplayName("Sản phẩm")]
        public SanPham SanPham { get; set; }

        [DisplayName("Số lượng")]
        [Required]
        public int SoLuong { get; set; }
    }
}
