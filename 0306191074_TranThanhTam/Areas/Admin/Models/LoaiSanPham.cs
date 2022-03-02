using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Areas.Admin.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tên loại sản phẩm")]
        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống")]
        public string TenLoaiSanPham { get; set; }

        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }

        public List<SanPham> SanPhams { get; set; }
    }
}
