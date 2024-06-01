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
using ToyStoreByVlasovAndry.Content;

namespace ToyStoreByVlasovAndry.EnterPages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            EnterEnable();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users_ToyStore userObj = AppConnect.model1db.Users_ToyStore.FirstOrDefault(x => x.user_login == txbLogin.Text && x.user_password == txbPassword.Password);

                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    switch (userObj.user_id_role)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте, \nадминистратор \n" + userObj.user_name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new ShopPage(userObj));
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте, \nпользователь \n" + userObj.user_name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new ShopPage(userObj));
                            break;
                        default:
                            MessageBox.Show("Ощибка данных на сервере.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: \n" + ex.Message.ToString(), "Критическая работа приложения!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SendReg_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new RegistrationPage());
        }

        private void ExitB_Click(object sender, RoutedEventArgs e)
        {
            var exitBi = MessageBox.Show("Вы уверенны, что хотите закрыть приложение?", "Выход из приложения", MessageBoxButton.YesNo, MessageBoxImage.Hand);

            if(exitBi == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void txbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EnterEnable();
        }

        private void txbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnterEnable();
        }

        private void EnterEnable()
        {
            if (txbPassword.Password.Length > 0 && txbLogin.Text.Length > 0)
            {
                Enter.IsEnabled = true;
            }
            else
            {
                Enter.IsEnabled = false;
            }
        }

        private void txbLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txbLogin.Text))
            {
                txbLogin.Visibility = Visibility.Collapsed;
                txbLoginPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void txbLoginPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            txbLoginPlaceHolder.Visibility= Visibility.Collapsed;
            txbLogin.Visibility= Visibility.Visible;
            txbLogin.Focus();
        }

        private void txbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbPassword.Password))
            {
                txbPassword.Visibility = Visibility.Collapsed;
                txbPasswordPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void txbPasswordPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            txbPasswordPlaceHolder.Visibility = Visibility.Collapsed;
            txbPassword.Visibility = Visibility.Visible;
            txbPassword.Focus();
        }
    }
}
