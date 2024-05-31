using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static iTextSharp.text.pdf.events.IndexEvents;

namespace ToyStoreByVlasovAndry.EnterPages
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }
        private void Regin_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.model1db.Users_ToyStore.Count(x => x.user_login == txbLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (AppConnect.model1db.Users_ToyStore.Count(x => x.user_name == tbxUserName.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким именем уже существует! Попробуйте другой вариант.", "Не-а", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                Users_ToyStore userObj = new Users_ToyStore()
                {
                    user_login = txbLogin.Text,
                    user_password = passbox1.Password,
                    user_id_role = 2,
                    user_name = tbxUserName.Text,
                    user_phone = tbxUserPhone.Text,
                    user_mail = tbxUserMail.Text
                };
                AppConnect.model1db.Users_ToyStore.Add(userObj);
                AppConnect.model1db.SaveChanges();
                MessageBox.Show("Вы успешно зарегестрировались!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new ShopPage(userObj));
            }
            catch
            {
                MessageBox.Show("Ошибка при внедрении данных на сервер!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void passbox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passbox2.Password == passbox1.Password && passbox1.Password.Length != 0)
            {
                passbox2.Background = Brushes.LightGreen;
                passbox2.BorderBrush = Brushes.Green;
                if (tbxUserName.Text.Length > 0 && txbLogin.Text.Length > 0)
                {
                    BtRegistration.IsEnabled = true;
                }
            }
            else
            {
                BtRegistration.IsEnabled = false;
                passbox2.Background = Brushes.LightCoral;
                passbox2.BorderBrush = Brushes.Red;
            }
        }

        private void passbox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passbox2.Password == passbox1.Password && passbox2.Password.Length != 0)
            {
                passbox2.Background = Brushes.LightGreen;
                passbox2.BorderBrush = Brushes.Green;
                if (tbxUserName.Text.Length > 0 && txbLogin.Text.Length > 0)
                {
                    BtRegistration.IsEnabled = true;
                }
            }
            else
            {
                BtRegistration.IsEnabled = false;
                passbox2.Background = Brushes.LightCoral;
                passbox2.BorderBrush = Brushes.Red;
            }
        }

        private void BtExit_Click(object sender, RoutedEventArgs e)
        {
            var exitBi = MessageBox.Show("Вы уверенны, что хотите закрыть приложение?", "Выход из приложения", MessageBoxButton.YesNo, MessageBoxImage.Hand);

            if (exitBi == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void tbxUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (passbox2.Password == passbox1.Password && passbox1.Password.Length != 0 && tbxUserName.Text.Length > 0 && txbLogin.Text.Length > 0)
            {
                BtRegistration.IsEnabled = true;
            }
            else
            {
                BtRegistration.IsEnabled = false;
            }
        }

        private void txbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (passbox2.Password == passbox1.Password && passbox1.Password.Length != 0 && tbxUserName.Text.Length > 0 && txbLogin.Text.Length > 0)
            {
                BtRegistration.IsEnabled = true;
            }
            else
            {
                BtRegistration.IsEnabled = false;
            }
        }
    }
}
