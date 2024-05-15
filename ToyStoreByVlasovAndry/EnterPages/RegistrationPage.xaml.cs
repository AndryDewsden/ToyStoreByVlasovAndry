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

namespace ToyStoreByVlasovAndry.EnterPages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
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
            try
            {
                Users_ToyStore userObj = new Users_ToyStore()
                {
                    user_login = txbLogin.Text,
                    user_password = passbox1.Password,
                    user_id_role = 2,
                    user_name = "fuck"
                };
                AppConnect.model1db.Users_ToyStore.Add(userObj);
                AppConnect.model1db.SaveChanges();
                MessageBox.Show("Данные успешно добавленны!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void passbox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passbox2.Password != passbox1.Password || passbox1.Password == "")
            {
                Regin.IsEnabled = false;
                passbox2.Background = Brushes.LightCoral;
                passbox2.BorderBrush = Brushes.Red;
            }
            else
            {
                Regin.IsEnabled = true;
                passbox2.Background = Brushes.LightGreen;
                passbox2.BorderBrush = Brushes.Green;
            }
        }

        private void passbox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passbox2.Password != passbox1.Password || passbox1.Password == "")
            {
                Regin.IsEnabled = false;
                passbox2.Background = Brushes.LightCoral;
                passbox2.BorderBrush = Brushes.Red;
            }
            else
            {
                Regin.IsEnabled = true;
                passbox2.Background = Brushes.LightGreen;
                passbox2.BorderBrush = Brushes.Green;
            }
        }
    }
}
