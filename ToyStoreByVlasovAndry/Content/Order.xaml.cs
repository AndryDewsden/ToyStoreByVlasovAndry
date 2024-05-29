using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToyStoreByVlasovAndry.ApplicationData;

namespace ToyStoreByVlasovAndry.Content
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        Users_ToyStore use = new Users_ToyStore();
        string numOrder = null;
        public Order(Users_ToyStore user)
        {
            InitializeComponent();

            use = user;
            userDisplay.Content = use.user_name;

            Directories_ToyStore userList = AppConnect.model1db.Directories_ToyStore.FirstOrDefault(x => x.directory_id_user == use.id_user);

            if (userList != null)
            {
                numOrder = userList.directory_order_number;
                listCart.ItemsSource = FillCart();
            }
        }

        Orders_ToyStore[] FillCart()
        {
            var productsInCart = AppConnect.model1db.Orders_ToyStore.ToList();

            if (numOrder != null)
            {
                productsInCart = AppConnect.model1db.Orders_ToyStore.Where(x => x.order_number == numOrder).ToList();
            }

            int WholeSaleSum = 0;
            int RetailSaleSum = 0;

            for(int i = 0; i < productsInCart.Count; i++)
            {
                int quan = productsInCart[i].order_quantity;
                int toy_id = productsInCart[i].order_id_toy;
                Toys_ToyStore pro = AppConnect.model1db.Toys_ToyStore.FirstOrDefault(x => x.id_toy == toy_id);
                int whol = pro.toy_wholesalePrice;
                int ret = pro.toy_retailPrice;

                WholeSaleSum += quan * whol;
                RetailSaleSum += quan * ret;
            }

            GoodCount.Content = productsInCart.Count;
            WholeSale.Content = WholeSaleSum;
            Retail.Content = RetailSaleSum;
            
            return productsInCart.ToArray();
        }

        private void goCart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new UserPage(use));
        }

        private void userDisplay_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }
    }
}
