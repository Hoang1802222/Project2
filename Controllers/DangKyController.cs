using Microsoft.AspNetCore.Mvc;
using EventHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace EventHub.Controllers
{
    [Authorize(Roles = "NguoiThamDu")]
    public class DangKyController : Controller
    {
        private readonly EventHubContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public DangKyController(EventHubContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Tao(int maSuKien)
        {
            var userId = _userManager.GetUserId(User);
            var suKien = await _db.SuKiens.FirstOrDefaultAsync(s => s.MaSuKien == maSuKien);
            if (suKien == null)
            {
                return NotFound();
            }
            if (suKien.TrangThaiDuyet != "DaDuyet" || suKien.TrangThaiDuyet =="DangMo")
            {
                TempData["Loi"] = "Sự kiện chưa mở đăng ký!";
                return RedirectToAction("ChiTiet", "SuKien", new { maSuKien = maSuKien });
            }
            
            var dangKy = await _db.DangKies.Include(d=>d.Ve).FirstOrDefaultAsync(d => d.MaSuKien == maSuKien && d.MaNguoiDung == userId);
            if (dangKy != null && dangKy.TrangThai == "DaDangKy")
            {
                TempData["Loi"] = "Bạn đã đăng ký sự kiện này rồi.";
                return RedirectToAction("Details", "SuKien", new { id = maSuKien });
            }
            var soCho = await _db.VwSoChoConLais.FirstOrDefaultAsync(s => s.MaSuKien == maSuKien);
            if (soCho == null || soCho.SoChoConLai <= 0)
            {
                TempData["Loi"] = "Sự kiện đã hết chỗ!";
                return RedirectToAction("ChiTiet", "SuKien", new { maSuKien = maSuKien });
            }
            if (dangKy == null)
            {
                //chua tung dangki
                dangKy = new DangKy
                {
                    MaNguoiDung = userId!,
                    MaSuKien = maSuKien,
                    TrangThai = "DaDangKy",
                    NgayDangKy = DateTime.UtcNow,
                };
                _db.DangKies.Add(dangKy);
            }
            else
            {
                dangKy.TrangThai = "DaDangKy";
                dangKy.NgayDangKy = DateTime.UtcNow;
                dangKy.NgayHuy= null;
            }
            // Mã vé mới: GUID là chuỗi ngẫu nhiên rất dài, không thể đoán được.
            // "N" nghĩa là 32 ký tự liền nhau, không có dấu gạch ngang.
            var soVe = Guid.NewGuid().ToString("N");
            if(dangKy.Ve == null)
            {
                //chua co ve tao ve moi
                dangKy.Ve = new Ve
                {
                    SoVe = soVe,
                    TrangThai="HopLe",
                    NgayPhatHanh = DateTime.UtcNow,
                };
            }
            else
            {
                dangKy.Ve.SoVe = soVe;
                dangKy.Ve.TrangThai = "HopLe";
                dangKy.Ve.NgayPhatHanh = DateTime.UtcNow;
            }
            await _db.SaveChangesAsync();
            TempData["ThanhCong"] = "Đăng ký sự kiện thành công!";
            return RedirectToAction("Index", "Ve");
        }
    }
}
