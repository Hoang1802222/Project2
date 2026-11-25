using Microsoft.AspNetCore.Mvc;
using EventHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EventHub.Data;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Microsoft.IdentityModel.Tokens;

namespace EventHub.Controllers
{
    [Authorize(Roles = "NguoiThamDu")]
    public class VeController : Controller
    {
        private readonly EventHubContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public VeController(EventHubContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var danhSachVe= await _db.Ves
                .Where(v => v.MaDangKyNavigation.MaNguoiDung == userId && v.TrangThai == "DaDangKy")
                .OrderByDescending(v => v.MaDangKyNavigation.MaSuKienNavigation.TrangThaiDuyet)
                .Select(v => new VeViewModel
                {
                    TieuDe = v.MaDangKyNavigation.MaSuKienNavigation.TieuDe,
                    DiaDiem = v.MaDangKyNavigation.MaSuKienNavigation.DiaDiem,
                    ThoiGianBatDau = v.MaDangKyNavigation.MaSuKienNavigation.ThoiGianBatDau,
                    SoVe = v.SoVe,
                    TrangThaiVe = v.TrangThai,
                    
                })
                .ToListAsync();
            foreach (var ve in danhSachVe)
            {
                ve.QrBase64 = GenerateQrCode(ve.SoVe);
            }
            return View(danhSachVe);
        }
        private string GenerateQrCode(string noiDung)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var data= qrGenerator.CreateQrCode(noiDung,QRCodeGenerator.ECCLevel.Q);
            var png= new PngByteQRCode(data).GetGraphic(20) ;
            return $"data:image/png;base64,{Convert.ToBase64String(png)}";

        }
    }
}
