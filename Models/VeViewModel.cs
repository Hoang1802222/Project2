namespace EventHub.Models
{
    public class VeViewModel
    {
        public string TieuDe { get; set; } = "";        // tên sự kiện
        public string DiaDiem { get; set; } = "";       // địa điểm
        public DateTime ThoiGianBatDau { get; set; }    // giờ bắt đầu
        public string SoVe { get; set; } = "";          // mã vé (nội dung của QR)
        public string TrangThaiVe { get; set; } = "";   // HopLe / DaSuDung / DaHuy
        public string QrBase64 { get; set; } = "";      // ảnh QR đã mã hóa thành chuỗi chữ
    }
}