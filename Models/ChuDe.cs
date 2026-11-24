using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class ChuDe
{
    public int MaChuDe { get; set; }

    public string TenChuDe { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<SuKien> SuKiens { get; set; } = new List<SuKien>();
}
