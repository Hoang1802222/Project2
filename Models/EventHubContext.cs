using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Models;

public partial class EventHubContext : DbContext
{
    public EventHubContext(DbContextOptions<EventHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    public virtual DbSet<ChuDe> ChuDes { get; set; }

    public virtual DbSet<DangKy> DangKies { get; set; }

    public virtual DbSet<DienGium> DienGia { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhanCongCheckIn> PhanCongCheckIns { get; set; }

    public virtual DbSet<PhienHoiThao> PhienHoiThaos { get; set; }

    public virtual DbSet<SuKien> SuKiens { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    public virtual DbSet<Ve> Ves { get; set; }

    public virtual DbSet<VwSoChoConLai> VwSoChoConLais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.MaCheckIn);

            entity.ToTable("CheckIn");

            entity.HasIndex(e => e.ThoiGianCheckIn, "IX_CheckIn_ThoiGian");

            entity.HasIndex(e => e.MaVe, "UQ_CheckIn_MaVe").IsUnique();

            entity.Property(e => e.GhiChu).HasMaxLength(300);
            entity.Property(e => e.HinhThuc)
                .HasMaxLength(10)
                .HasDefaultValue("QR", "DF_CheckIn_HinhThuc");
            entity.Property(e => e.ThoiGianCheckIn).HasDefaultValueSql("(sysutcdatetime())", "DF_CheckIn_ThoiGian");

            entity.HasOne(d => d.MaPhanCongNavigation).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.MaPhanCong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CheckIn_PhanCong");

            entity.HasOne(d => d.MaVeNavigation).WithOne(p => p.CheckIn)
                .HasForeignKey<CheckIn>(d => d.MaVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CheckIn_Ve");
        });

        modelBuilder.Entity<ChuDe>(entity =>
        {
            entity.HasKey(e => e.MaChuDe);

            entity.ToTable("ChuDe");

            entity.HasIndex(e => e.TenChuDe, "UQ_ChuDe_TenChuDe").IsUnique();

            entity.Property(e => e.MoTa).HasMaxLength(300);
            entity.Property(e => e.TenChuDe).HasMaxLength(100);
        });

        modelBuilder.Entity<DangKy>(entity =>
        {
            entity.HasKey(e => e.MaDangKy);

            entity.ToTable("DangKy");

            entity.HasIndex(e => new { e.MaSuKien, e.TrangThai }, "IX_DangKy_SuKien");

            entity.HasIndex(e => new { e.MaNguoiDung, e.MaSuKien }, "UQ_DangKy_NguoiDung_SuKien").IsUnique();

            entity.Property(e => e.NgayDangKy).HasDefaultValueSql("(sysutcdatetime())", "DF_DangKy_NgayDangKy");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("DaDangKy", "DF_DangKy_TrangThai");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DangKies)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DangKy_NguoiDung");

            entity.HasOne(d => d.MaSuKienNavigation).WithMany(p => p.DangKies)
                .HasForeignKey(d => d.MaSuKien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DangKy_SuKien");
        });

        modelBuilder.Entity<DienGium>(entity =>
        {
            entity.HasKey(e => e.MaDienGia);

            entity.Property(e => e.AnhDaiDien).HasMaxLength(500);
            entity.Property(e => e.DonVi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.HoTen).HasMaxLength(200);
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung);

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Email, "UQ_NguoiDung_Email").IsUnique();

            entity.Property(e => e.AnhDaiDien).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.HoTen).HasMaxLength(200);
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(sysutcdatetime())", "DF_NguoiDung_NgayTao");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);

            entity.HasOne(d => d.MaVaiTroNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.MaVaiTro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NguoiDung_VaiTro");
        });

        modelBuilder.Entity<PhanCongCheckIn>(entity =>
        {
            entity.HasKey(e => e.MaPhanCong);

            entity.ToTable("PhanCongCheckIn");

            entity.HasIndex(e => new { e.MaSuKien, e.MaNhanVien }, "UQ_PhanCong_SuKien_NhanVien").IsUnique();

            entity.Property(e => e.NgayPhanCong).HasDefaultValueSql("(sysutcdatetime())", "DF_PhanCong_Ngay");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.PhanCongCheckIns)
                .HasForeignKey(d => d.MaNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanCong_NhanVien");

            entity.HasOne(d => d.MaSuKienNavigation).WithMany(p => p.PhanCongCheckIns)
                .HasForeignKey(d => d.MaSuKien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanCong_SuKien");
        });

        modelBuilder.Entity<PhienHoiThao>(entity =>
        {
            entity.HasKey(e => e.MaPhien);

            entity.ToTable("PhienHoiThao");

            entity.HasIndex(e => e.MaSuKien, "IX_PhienHoiThao_SuKien");

            entity.Property(e => e.PhongHop).HasMaxLength(100);
            entity.Property(e => e.TieuDe).HasMaxLength(200);

            entity.HasOne(d => d.MaSuKienNavigation).WithMany(p => p.PhienHoiThaos)
                .HasForeignKey(d => d.MaSuKien)
                .HasConstraintName("FK_PhienHoiThao_SuKien");

            entity.HasMany(d => d.MaDienGia).WithMany(p => p.MaPhiens)
                .UsingEntity<Dictionary<string, object>>(
                    "PhienDienGium",
                    r => r.HasOne<DienGium>().WithMany()
                        .HasForeignKey("MaDienGia")
                        .HasConstraintName("FK_PhienDienGia_DienGia"),
                    l => l.HasOne<PhienHoiThao>().WithMany()
                        .HasForeignKey("MaPhien")
                        .HasConstraintName("FK_PhienDienGia_Phien"),
                    j =>
                    {
                        j.HasKey("MaPhien", "MaDienGia");
                        j.ToTable("PhienDienGia");
                    });
        });

        modelBuilder.Entity<SuKien>(entity =>
        {
            entity.HasKey(e => e.MaSuKien);

            entity.ToTable("SuKien");

            entity.HasIndex(e => e.MaChuDe, "IX_SuKien_ChuDe");

            entity.HasIndex(e => e.MaNguoiToChuc, "IX_SuKien_NguoiToChuc");

            entity.HasIndex(e => e.ThoiGianBatDau, "IX_SuKien_ThoiGianBatDau");

            entity.Property(e => e.AnhBia).HasMaxLength(500);
            entity.Property(e => e.DiaDiem).HasMaxLength(300);
            entity.Property(e => e.NgayCapNhat).HasDefaultValueSql("(sysutcdatetime())", "DF_SuKien_NgayCapNhat");
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(sysutcdatetime())", "DF_SuKien_NgayTao");
            entity.Property(e => e.TieuDe).HasMaxLength(200);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Nhap", "DF_SuKien_TrangThai");
            entity.Property(e => e.TrangThaiDuyet)
                .HasMaxLength(20)
                .HasDefaultValue("ChoDuyet", "DF_SuKien_TrangThaiDuyet");

            entity.HasOne(d => d.MaChuDeNavigation).WithMany(p => p.SuKiens)
                .HasForeignKey(d => d.MaChuDe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuKien_ChuDe");

            entity.HasOne(d => d.MaNguoiToChucNavigation).WithMany(p => p.SuKiens)
                .HasForeignKey(d => d.MaNguoiToChuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuKien_NguoiToChuc");
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro);

            entity.ToTable("VaiTro");

            entity.HasIndex(e => e.TenVaiTro, "UQ_VaiTro_TenVaiTro").IsUnique();

            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
        });

        modelBuilder.Entity<Ve>(entity =>
        {
            entity.HasKey(e => e.MaVe);

            entity.ToTable("Ve");

            entity.HasIndex(e => e.MaDangKy, "UQ_Ve_MaDangKy").IsUnique();

            entity.HasIndex(e => e.SoVe, "UQ_Ve_SoVe").IsUnique();

            entity.Property(e => e.NgayPhatHanh).HasDefaultValueSql("(sysutcdatetime())", "DF_Ve_NgayPhatHanh");
            entity.Property(e => e.PhienBan)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.SoVe).HasMaxLength(64);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("HopLe", "DF_Ve_TrangThai");

            entity.HasOne(d => d.MaDangKyNavigation).WithOne(p => p.Ve)
                .HasForeignKey<Ve>(d => d.MaDangKy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ve_DangKy");
        });

        modelBuilder.Entity<VwSoChoConLai>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_SoChoConLai");

            entity.Property(e => e.TieuDe).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
