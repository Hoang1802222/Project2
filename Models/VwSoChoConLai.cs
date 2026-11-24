using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class VwSoChoConLai
{
    public int MaSuKien { get; set; }

    public string TieuDe { get; set; } = null!;

    public int SucChua { get; set; }

    public int? SoDaDangKy { get; set; }

    public int? SoChoConLai { get; set; }
}
