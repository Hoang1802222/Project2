using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class NguoiDung
{
    public string MaNguoiDung { get; set; } = null!;

    public int MaVaiTro { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

   

    public string? SoDienThoai { get; set; }

    public string? AnhDaiDien { get; set; }

    public bool BiKhoa { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual ICollection<DangKy> DangKies { get; set; } = new List<DangKy>();

    public virtual VaiTro MaVaiTroNavigation { get; set; } = null!;

    public virtual ICollection<PhanCongCheckIn> PhanCongCheckIns { get; set; } = new List<PhanCongCheckIn>();

    public virtual ICollection<SuKien> SuKiens { get; set; } = new List<SuKien>();
}
