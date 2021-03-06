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
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.Evaluation = new HashSet<Evaluation>();
        }
    
        public int OrderID { get; set; }
        public Nullable<int> CaseID { get; set; }
        public int MemberID { get; set; }
        public Nullable<int> ShoppingCartID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public int Point { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> FinishedDate { get; set; }
        public int OrderStatusID { get; set; }
    
        public virtual Cases Cases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluation> Evaluation { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
