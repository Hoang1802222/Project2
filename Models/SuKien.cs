using System;
using System.Collections.Generic;

namespace EventHub.Models;

public partial class SuKien
{
    public int MaSuKien { get; set; }

    public string MaNguoiToChuc { get; set; } = null!;

    public int MaChuDe { get; set; }

    public string TieuDe { get; set; } = null!;

    public string? MoTa { get; set; }

    public string DiaDiem { get; set; } = null!;

    public string? AnhBia { get; set; }

    public DateTime ThoiGianBatDau { get; set; }

    public DateTime ThoiGianKetThuc { get; set; }

    public int SucChua { get; set; }

    public string TrangThai { get; set; } = null!;

    public string TrangThaiDuyet { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public DateTime NgayCapNhat { get; set; }

    public virtual ICollection<DangKy> DangKies { get; set; } = new List<DangKy>();

    public virtual ChuDe MaChuDeNavigation { get; set; } = null!;

    public virtual NguoiDung MaNguoiToChucNavigation { get; set; } = null!;

    public virtual ICollection<PhanCongCheckIn> PhanCongCheckIns { get; set; } = new List<PhanCongCheckIn>();

    public virtual ICollection<PhienHoiThao> PhienHoiThaos { get; set; } = new List<PhienHoiThao>();
}
