using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class DangKy
{
    public int MaDangKy { get; set; }

    public string MaNguoiDung { get; set; } = null!;

    public int MaSuKien { get; set; }

    public string TrangThai { get; set; } = null!;

    public DateTime NgayDangKy { get; set; }

    public DateTime? NgayHuy { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual SuKien MaSuKienNavigation { get; set; } = null!;

    public virtual Ve? Ve { get; set; }
}
