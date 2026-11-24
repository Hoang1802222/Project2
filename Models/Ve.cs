using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class Ve
{
    public int MaVe { get; set; }

    public int MaDangKy { get; set; }

    public string SoVe { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public DateTime NgayPhatHanh { get; set; }

    public DateTime? NgayGuiEmail { get; set; }

    public byte[] PhienBan { get; set; } = null!;

    public virtual CheckIn? CheckIn { get; set; }

    public virtual DangKy MaDangKyNavigation { get; set; } = null!;
}
