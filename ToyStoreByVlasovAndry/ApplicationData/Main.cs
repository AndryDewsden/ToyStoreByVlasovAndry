//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToyStoreByVlasovAndry.ApplicationData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Main
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Main()
        {
            this.Deals = new HashSet<Deals>();
        }
    
        public int id_main { get; set; }
        public string article { get; set; }
        public int name_id { get; set; }
        public int edenic_id { get; set; }
        public int prize { get; set; }
        public int maxSkid { get; set; }
        public int maker_id { get; set; }
        public int giver_id { get; set; }
        public int category_id { get; set; }
        public int actSkid { get; set; }
        public int kolOnSklad { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    
        public virtual Categorys Categorys { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deals> Deals { get; set; }
        public virtual Edenices Edenices { get; set; }
        public virtual Givers Givers { get; set; }
        public virtual Makers Makers { get; set; }
        public virtual Names Names { get; set; }
    }
}