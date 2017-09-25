using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Models
{
    [MetadataType(typeof(order))]
    public partial class Orders
    {
        public  class order
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public order()
            {
                this.Evaluation = new HashSet<Evaluation>();
            }
            [DisplayName("訂單ID")]
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
}