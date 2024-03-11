using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models.ViewModel
{
    public class AccountViewModel
    {
        public int? SN { get; set; }

        [Display(Name = "姓名")]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "年齡")]
        [Required]
        public int Age { get; set; }

        [Display(Name = "生日")]
        [Required]
        public DateTime Birthday { get; set; }
    }
}