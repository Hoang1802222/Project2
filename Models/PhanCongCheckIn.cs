using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class PhanCongCheckIn
{
    public int MaPhanCong { get; set; }

    public int MaSuKien { get; set; }

    public string MaNhanVien { get; set; } = null!;

    public DateTime NgayPhanCong { get; set; }

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual NguoiDung MaNhanVienNavigation { get; set; } = null!;

    public virtual SuKien MaSuKienNavigation { get; set; } = null!;
}
