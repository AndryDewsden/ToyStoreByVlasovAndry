using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class CartPage : Page
    {
        Users_ToyStore use = new Users_ToyStore();
        string numOrder = null;
        public CartPage(Users_ToyStore user)
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
                if (productsInCart.Count > 0)
                {
                    Stat.Content = "В вашей корзине " + productsInCart.Count + " товаров.";
                }
                else
                {
                    Stat.Content = "Ваша корзина пуста.";
                }
            }

            return productsInCart.ToArray();
        }

        private void goGood_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new ShopPage(use));
        }

        private void userDisplay_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new UserPage(use));
        }

        private void delProduct_Click(object sender, RoutedEventArgs e)
        {
            if((Orders_ToyStore)listCart.SelectedItem != null)
            {
                Orders_ToyStore del = (Orders_ToyStore)listCart.SelectedItem;
                delCart(del);
            }
        }

        private void delCart(Orders_ToyStore del)
        {
            var res = MessageBox.Show($"Вы действительно хотите удалить этот товар?\n Будет удалён:\nНаименование: {del.order_number} \nАртикль: {del.order_id_toy} \n{del.order_quantity}", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.model1db.Orders_ToyStore.Remove(del);
                    AppConnect.model1db.SaveChanges();
                    listCart.ItemsSource = FillCart();
                    //MessageBox.Show("Данные успешно удалены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OrderMake_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Order(use));
        }

        private void upNum_Click(object sender, RoutedEventArgs e)
        {
            if ((Orders_ToyStore)listCart.SelectedItem != null)
            {
                Orders_ToyStore red = (Orders_ToyStore)listCart.SelectedItem;

                if (red.order_quantity < 100)
                {
                    var res = MessageBox.Show("Вы действительно хотите увеличить это число?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (res == MessageBoxResult.OK)
                    {
                        try
                        {
                            red.order_quantity = red.order_quantity + 1;
                            AppConnect.model1db.SaveChanges();
                            //MessageBox.Show("Данные успешно редактированы", "Тестирование", MessageBoxButton.OK, MessageBoxImage.Information);
                            listCart.ItemsSource = FillCart();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void downNum_Click(object sender, RoutedEventArgs e)
        {
            if ((Orders_ToyStore)listCart.SelectedItem != null)
            {
                Orders_ToyStore red = (Orders_ToyStore)listCart.SelectedItem;
                
                if (red.order_quantity > 1)
                {
                    var res = MessageBox.Show("Вы действительно хотите убавить это число?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (res == MessageBoxResult.OK)
                    {
                        try
                        {
                            red.order_quantity = red.order_quantity - 1;
                            AppConnect.model1db.SaveChanges();
                            //MessageBox.Show("Данные успешно редактированы", "Тестирование", MessageBoxButton.OK, MessageBoxImage.Information);
                            listCart.ItemsSource = FillCart();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
    }
}
