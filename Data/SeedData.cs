using EventHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Data
{
    public class SeedData
    {
        public static readonly string[] Roles = { "QuanTriVien", "BanToChuc" ,"NhanVienCheckIn","NguoiThamDu"};
        public static async Task Initialize(IServiceProvider sp)
        {
            var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = sp.GetRequiredService<UserManager<IdentityUser>>();
            var dbContext = sp.GetRequiredService<EventHubContext>();
            // tao roles
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            // tao tai khoan admin neu chua co
            const string adminEmail = "lhhoang@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "QuanTriVien");
                }
            }
            // dong bo voi bang nguoi dung trong csdl
            if (!dbContext.NguoiDungs.Any())
            {
                var adminNguoiDung = new NguoiDung
                {
                    MaNguoiDung = Guid.NewGuid().ToString(),
                    MaVaiTro = 1, // QuanTriVien
                    HoTen = "Admin",
                    Email = adminEmail,
                    BiKhoa = false,
                    NgayTao = DateTime.Now
                };
                dbContext.NguoiDungs.Add(adminNguoiDung);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
