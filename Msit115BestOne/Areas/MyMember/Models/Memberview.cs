using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyMember.Models
{
    public class Memberview
    {
        [DisplayName("ID")]
        public int MemberID { get; set; }
        [DisplayName("姓氏：")]
        [Required(ErrorMessage = "姓氏必填")]
        public string FirstName { get; set; }
        [DisplayName("名字：")]
        [Required(ErrorMessage = "名字必填")]
        public string LastName { get; set; }
        [DisplayName("帳號(身分證字號)：")]
        [Required(ErrorMessage = "帳號必填")]
        public string MAccount { get; set; }
        [DisplayName("生日：")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "生日必填")]
        public Nullable<System.DateTime> Birthday { get; set; }
        [DisplayName("地址：")]
        [Required(ErrorMessage = "地址必填")]
        public string Address { get; set; }
        [DisplayName("電話：")]
        [Required(ErrorMessage = "電話必填")]
        public string Phone { get; set; }
        [DisplayName("電子信箱：")]
        [Required(ErrorMessage = "信箱必填")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("密碼：")]
        [Required(ErrorMessage = "密碼必填")]
        public string MPassword { get; set; }

        [DisplayName("等級：")]
        public int Stage { get; set; }
        [DisplayName("等級：")]
        public string Title { get; set; }
        [DisplayName("經驗值：")]
        [DisplayFormat(DataFormatString = "{0:c0}")]
        public int EXP { get; set; }


        public byte[] Photo { get; set; }



        [DisplayName("點數：")]
        [DisplayFormat(DataFormatString = "{0:c0}")]
        public int PointCount { get; set; }
        public int RegionID { get; set; }
        [DisplayName("暱稱：")]
        public string NickName { get; set; }
      
        [DisplayName("區：")]
        public string RegionName { get; set; }
        [DisplayName("縣市：")]
        public string CityName { get; set; }

        [DisplayName("大頭貼：")]
        public string strPhoto { get; set; }

        [DisplayName("縣市")]
        public int CityID { get; set; }

        //[DataType(DataType.Password)]
        [Compare("MPassword")]
        [DisplayName("密碼確認：")]
        public string ConfirmPassword { get; set; }


        //================================
        [DisplayName("總評價點:")]
        public int sumpoint { get; set; } //依照會員發的case獲得的評價加總 當作分數依據
                                          //case數量
                                          //case 人力總數
        [DisplayName("總案件:")]
        public int casecount { get; set; }
        [DisplayName("總物品案件:")]
        public int GDcasecount { get; set; }
        [DisplayName("總需求物品案件:")]
        public int GDcaseneed { get; set; }
        [DisplayName("總捐贈物品案件:")]
        public int GDcasegive { get; set; }
        [DisplayName("總人力案件:")]
        public int MPcasecount { get; set; }
        [DisplayName("總需求人力案件:")]
        public int MPcaseneed { get; set; }
        [DisplayName("總提供人力案件:")]
        public int MPcasegive { get; set; }

        //========專長
        public int MPSubClassID { get; set; }
        [DisplayName("專長：")]
        public string MPSubClass1 { get; set; }

    }
}