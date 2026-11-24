using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class PhienHoiThao
{
    public int MaPhien { get; set; }

    public int MaSuKien { get; set; }

    public string TieuDe { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? PhongHop { get; set; }

    public DateTime ThoiGianBatDau { get; set; }

    public DateTime ThoiGianKetThuc { get; set; }

    public virtual SuKien MaSuKienNavigation { get; set; } = null!;

    public virtual ICollection<DienGium> MaDienGia { get; set; } = new List<DienGium>();
}
