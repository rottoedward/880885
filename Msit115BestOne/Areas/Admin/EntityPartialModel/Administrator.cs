using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Models
{
    [MetadataType(typeof(AdministratorMetaData))]
    public partial class Administrator
    {
        public class AdministratorMetaData
        {
            [DisplayName("帳號")]
            [Required(ErrorMessage ="帳號必填")]
            public string AdminID { get; set; }

            [DisplayName("密碼")]
            [Required(ErrorMessage = "密碼必填")]
            public string Apassword { get; set; }

            [DisplayName("權限")]
            public byte Authority { get; set; }
        }
    }
}