using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PS11905_BAODUONG_ASM.Models
{
    public enum TrangthaiDonhang // Trạng thái đơn hàng
    {
        [Display(Name ="Mới đặt")]
        Moidat = 1,
        [Display(Name = "Đang giao")]
        Danggiao = 2,
        [Display(Name = "Đã giao")]
        Dagiao = 3
    }

    public class Donhang
    {
        [Key]
        public int DonhangID { get; set; }

        [ForeignKey("Khachhang")]
        public int KhachhangID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        [Required(ErrorMessage = "Bạn cần chọn ngày."), Display(Name ="Ngày đặt")]
        public DateTime Ngaydat { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage ="Bạn cần nhập giá.")]
        [Display(Name = "Tổng tiền")]
        public double Tongtien { get; set; }

        [Display(Name = "Trạng thái")]
        public TrangthaiDonhang Trangthai { get; set; }

        [StringLength(250)]
        [Display(Name = "Ghi chú")]
        public string Ghichu { get; set; }

        public Khachhang Khachhang { get; set; }

        public List<DonhangChitiet> DonhangChitiets { get; set; }
    }
}
