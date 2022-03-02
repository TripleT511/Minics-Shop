using _0306191074_TranThanhTam.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _0306191074_TranThanhTam.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ILogger<TestimonialController> _logger;
        private readonly _0306191074_TranThanhTamContext _context;

        public TestimonialController(ILogger<TestimonialController> logger, _0306191074_TranThanhTamContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lstDanhGia = await _context.DanhGias.Where(dg => dg.SaoDanhGia == 5).Where(a => a.TrangThai == true).Include(tk => tk.TaiKhoan).ToListAsync();
            ViewData["danhgia"] = lstDanhGia;
            return View();
        }
    }
}
