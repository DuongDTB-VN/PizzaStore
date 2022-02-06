using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PS11905_BAODUONG_ASM.Models
{

    public enum PhanLoai
    {
        [Display(Name = "Món")]
        Monan = 1,
        [Display(Name = "Combo")]
        Combo = 2,
        [Display(Name = "Nước")]
        Nuoc = 3,
   }

    public class MonAn
    {
        public int MonAnID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Bạn cần nhập tên món ăn.")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập giá."), Range(0,double.MaxValue,ErrorMessage = "Bạn cần nhập giá.")]
        [Display(Name = "Giá")]
        public double Gia { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn phân loại."), Range(1, int.MaxValue, ErrorMessage = "Bạn cần chọn phân loại.")]
        [Display(Name = "Phân Loại")]
        public PhanLoai Phanloai { get; set; }

        [StringLength(300)]
        [Display(Name = "Hình")]
        public string HinhAnh { get; set; }

        [NotMapped]
        [Display(Name = "Chọn hình")]
        public IFormFile ImageFile { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        
        [Display(Name = "Đang phục vụ")]
        public bool TrangThai { get; set; }
    }
}
