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
    
    public partial class Manufacturers_ToyStore
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manufacturers_ToyStore()
        {
            this.Toys_ToyStore = new HashSet<Toys_ToyStore>();
        }
    
        public int id_manufacturer { get; set; }
        public string manufacturer_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Toys_ToyStore> Toys_ToyStore { get; set; }
    }
}
