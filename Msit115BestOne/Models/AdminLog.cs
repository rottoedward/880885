//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Msit115BestOne.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminLog
    {
        public int LogId { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public string AdminName { get; set; }
        public string SaveData { get; set; }
        public string Data_Action { get; set; }
        public string Orignal_Page { get; set; }
        public string ActionName { get; set; }
        public string ControllersName { get; set; }
    }
}
