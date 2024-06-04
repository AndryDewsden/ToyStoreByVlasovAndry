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
    
    public partial class Toys_ToyStore
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Toys_ToyStore()
        {
            this.Orders_ToyStore = new HashSet<Orders_ToyStore>();
        }
    
        public int id_toy { get; set; }
        public string toy_name { get; set; }
        public int toy_id_category { get; set; }
        public int toy_id_country { get; set; }
        public int toy_id_manufacturer { get; set; }
        public int toy_id_provider { get; set; }
        public int toy_wholesalePrice { get; set; }
        public int toy_retailPrice { get; set; }
        public int toy_id_ageCategory { get; set; }
        public string toy_discription { get; set; }
        public string toy_image { get; set; }
        public string pic
        {
            get
            {
                if (!String.IsNullOrEmpty(toy_image) || !String.IsNullOrWhiteSpace(toy_image))
                {
                    //return "/images/picture.png";
                    return $"/images/{toy_image}";
                }
                else
                {
                    return "/images/picture.png";
                    //return $"/images/{toy_image}";
                }
            }
        }
        public virtual AgeCategories_ToyStore AgeCategories_ToyStore { get; set; }
        public virtual Categories_ToyStore Categories_ToyStore { get; set; }
        public virtual Countries_ToyStore Countries_ToyStore { get; set; }
        public virtual Manufacturers_ToyStore Manufacturers_ToyStore { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders_ToyStore> Orders_ToyStore { get; set; }
        public virtual Providers_ToyStore Providers_ToyStore { get; set; }
    }
}
