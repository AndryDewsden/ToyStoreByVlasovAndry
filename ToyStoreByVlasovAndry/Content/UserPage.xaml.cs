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
using ToyStoreByVlasovAndry.EnterPages;
using ToyStoreByVlasovAndry.ModPage;

namespace ToyStoreByVlasovAndry.Content
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        Users_ToyStore use = new Users_ToyStore();
        public UserPage(Users_ToyStore user)
        {
            InitializeComponent();
            use = user;
            username.Content = $"Пользователь: {user.user_name}";
            switch (user.user_id_role)
            {
                case 1:
                    Title = "Страница администратора " + user.user_name;
                    addButton.IsEnabled = true;
                    break;
                case 2:
                    Title = "Страница пользователя " + user.user_name;
                    addButton.IsEnabled = false;
                    addButton.Visibility = Visibility.Collapsed;
                    break;
                default:
                    MessageBox.Show("Произошла какае-то ощибка с данными пользователя. Вас перекинут на страницу авторизации.", "О-оу", MessageBoxButton.OK, MessageBoxImage.Error);
                    AppFrame.frameMain.Navigate(new LoginPage());
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new ShopPage(use));
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Уведомление", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (res == MessageBoxResult.OK)
            {
                AppFrame.frameMain.Navigate(new LoginPage());
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new AddRedPage(null, use));
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new CartPage(use));
        }
    }
}
