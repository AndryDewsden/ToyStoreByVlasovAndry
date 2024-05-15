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
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
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
                            MessageBox.Show("Здраствуйте, Администратор " + userObj.user_name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new UserPage(userObj));
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте, " + userObj.user_name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new UserPage(userObj));
                            break;
                        default:
                            MessageBox.Show("Данные не обнаруженны!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Критическая работа приложения!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SendReg_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new RegistrationPage());
        }
    }
}
