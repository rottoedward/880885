using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Models
{ 
    [MetadataType(typeof(_Member))]
    public partial class Member
    {
        public class _Member
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            //public Member()
            //{
            //    this.Board = new HashSet<Board>();
            //    this.Cases = new HashSet<Cases>();
            //    this.Chat = new HashSet<Chat>();
            //    this.Content = new HashSet<Content>();
            //}

            public int MemberID { get; set; }
            [DisplayName("發文者")]
            public string FirstName { get; set; }
            [DisplayName("發文者")]
            public string LastName { get; set; }
            public string MAccount { get; set; }
            public Nullable<System.DateTime> Birthday { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string MPassword { get; set; }
            public int Stage { get; set; }
            public int EXP { get; set; }
            public byte[] Photo { get; set; }
            public int PointCount { get; set; }
            public int RegionID { get; set; }
            [DisplayName("發文者")]
            public string NickName { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Board> Board { get; set; }
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Cases> Cases { get; set; }
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Chat> Chat { get; set; }
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Content> Content { get; set; }
            public virtual MemberStage MemberStage { get; set; }
            public virtual Region Region { get; set; }
        }
    }
}