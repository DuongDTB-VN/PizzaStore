using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PS11905_BAODUONG_ASM.Models
{
    public class DonhangChitiet
    {
        [Key]
        public int ChitietID { get; set; }

        [ForeignKey("Donhang")]
        public int DonhangID { get; set; }

        [ForeignKey("MonAn")]
        public int MonAnID { get; set; }

        [Required,Range(0, int.MaxValue, ErrorMessage ="Bạn cần nhập số lượng.")]
        [Display(Name = "Số lượng")]
        public int Soluong { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Bạn cần nhập thành tiền.")]
        [Display(Name = "Thành tiền")]
        public double Thanhtien { get; set; }

        [StringLength(250)]
        [Display(Name = "Ghi chú")]
        public string Ghichu { get; set;}

        public Donhang Donhang { get; set; }
        public MonAn MonAn { get; set; }
    }
}
