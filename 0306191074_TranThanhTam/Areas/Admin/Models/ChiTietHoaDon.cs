using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Areas.Admin.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Mã hoá đơn")]
        [Required(ErrorMessage = "Mã hoá đơn không được để trống")]
        public int HoaDonId { get; set; }

        [DisplayName("Mã hoá đơn")]
        public HoaDon HoaDon { get; set; }

        [DisplayName("Mã sản phẩm")]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public int SanPhamId { get; set; }

        [DisplayName("Mã sản phẩm")]
        public SanPham SanPham { get; set; }

        [DisplayName("Số lượng")]
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int SoLuong { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} ₫")]
        [DisplayName("Đơn giá")]
        [Required(ErrorMessage = "Đơn giá không được để trống")]
        public int DonGia { get; set; }
    }
}
