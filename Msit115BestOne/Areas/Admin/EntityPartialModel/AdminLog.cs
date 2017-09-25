using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Models
{
    [MetadataType(typeof(AdminLogMetaData))]
    public partial class AdminLog
    {
        public class AdminLogMetaData {

            [DisplayName("記錄編號")]
            public int LogId { get; set; }

            [DisplayName("記錄建立時間")]
            public DateTime CreateDateTime { get; set; }

            [DisplayName("管理員名稱")]
            public string AdminName { get; set; }

            [DisplayName("內容")]
            public string SaveData { get; set; }

            [DisplayName("執行動作")]
            public string Data_Action { get; set; }

            [DisplayName("執行頁面")]
            public string Orignal_Page { get; set; }

            [DisplayName("Action")]
            public string ActionName { get; set; }

            [DisplayName("Controller")]
            public string ControllersName { get; set; }
        }
    }
}