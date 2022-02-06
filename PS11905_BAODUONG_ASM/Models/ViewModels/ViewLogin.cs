using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS11905_BAODUONG_ASM.Models.ViewModels
{
    public class ViewLogin
    {
        [Required]
        [Display(Name ="Tài khoản")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
