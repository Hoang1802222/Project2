using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class DienGium
{
    public int MaDienGia { get; set; }

    public string HoTen { get; set; } = null!;

    public string? DonVi { get; set; }

    public string? TieuSu { get; set; }

    public string? AnhDaiDien { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<PhienHoiThao> MaPhiens { get; set; } = new List<PhienHoiThao>();
}
