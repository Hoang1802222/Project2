using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class CheckIn
{
    public int MaCheckIn { get; set; }

    public int MaVe { get; set; }

    public int MaPhanCong { get; set; }

    public DateTime ThoiGianCheckIn { get; set; }

    public string HinhThuc { get; set; } = null!;

    public string? GhiChu { get; set; }

    public virtual PhanCongCheckIn MaPhanCongNavigation { get; set; } = null!;

    public virtual Ve MaVeNavigation { get; set; } = null!;
}
