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
    
    public partial class Orders_ToyStore
    {
        public int id_order { get; set; }
        public string order_number { get; set; }
        public int order_id_toy { get; set; }
        public int order_quantity { get; set; }
    
        public virtual Toys_ToyStore Toys_ToyStore { get; set; }
    }
}
