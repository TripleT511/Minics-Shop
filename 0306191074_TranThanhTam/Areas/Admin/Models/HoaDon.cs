using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Areas.Admin.Models
{
    public class HoaDon
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tài khoản")]
        [Required]
        public int TaiKhoanId { get; set; }

        [DisplayName("Tài khoản")]
        public TaiKhoan Account { get; set; }

        [DisplayName("Ngày xuất hoá đơn")]
        [Required(ErrorMessage = "Ngày xuất hoá đơn phải được chọn")]
        public DateTime NgayXuatHD { get; set; }

        [DisplayName("Địa chỉ giao hàng")]
        [Required(ErrorMessage = "Địa chỉ giao hàng không được bỏ trống")]
        public string DiaChiGH { get; set; }

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        public string SoDienThoai { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} ₫")]
        [DisplayName("Tổng tiền")]
        [Required(ErrorMessage = "Tổng tiền không được bỏ trống")]
        public int TongTien { get; set; }

        [DisplayName("Trạng thái")]
        public int TrangThai { get; set; }

        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
