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
    public class TaiKhoan
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản không được bỏ trống"), MinLength(5, ErrorMessage = "Tên tài khoản tối thiểu 5 kí tự"), MaxLength(15, ErrorMessage = "Tên tài khoản tối đa 15 kí tự")]
        public string TenTaiKhoan { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống"), RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$", ErrorMessage = "Mật khẩu tối thiểu 6 kí tự và bao gồm chữ hoa và chữ thường")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống"), RegularExpression(@"^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống"), RegularExpression(@"((09|03|07|08|05)+([0-9]{8,9})\b)",  ErrorMessage = "Số điện thoại không đúng định dạng"), MaxLength(11), MinLength(9)]
        public string SoDienThoai { get; set; }

        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống"), MinLength(5)]
        public string DiaChi { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ Tên không được bỏ trống")]
        public string HoTen { get; set; }

        [DisplayName("Phân quyền")]
        public bool PhanQuyen { get; set; }

        [DisplayName("Ảnh đại diện")]
        [Required(ErrorMessage = "Bắt buộc chọn hình ảnh"), RegularExpression(@"^[\d\w-]+\.(png|PNG|jpg|JPG)$", ErrorMessage = "Định dạng jpg hoặc png")]
        public string AnhDaiDien { get; set; }

        [DisplayName("Ảnh đại diện")]
        [NotMapped]
        public IFormFile FileHinhAnh { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        public List<GioHang> GioHangs { get; set; } 

        public List<HoaDon> HoaDons { get; set; }

        public List<DanhGia> DanhGias { get; set; }

    }
}
