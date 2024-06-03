using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class UserPage : Page
    {
        Users_ToyStore use = new Users_ToyStore();
        private Users_ToyStore _curUser = new Users_ToyStore();
        public UserPage(Users_ToyStore user)
        {
            InitializeComponent();
            use = user;
            username.Content = $"Пользователь: {user.user_name}";

            switch (user.user_id_role)
            {
                case 1:
                    Title = $"Страница администратора {user.user_name}";

                    addButton.IsEnabled = true;
                    addButton.Visibility = Visibility.Visible;

                    BtDelAcc.IsEnabled = false;
                    BtDelAcc.Visibility = Visibility.Collapsed;

                    userList.IsEnabled = true;
                    userList.Visibility = Visibility.Visible;

                    uploadImage.IsEnabled = true;
                    uploadImage.Visibility = Visibility.Visible;

                    //заранее извиняюсь за вульгарные название, я просто пипец как спать хочу, потом поменяю
                    Cum.IsEnabled = true;
                    Cum.Visibility = Visibility.Visible;

                    break;

                case 2:
                    Title = $"Страница пользователя {user.user_name}";
                    
                    addButton.IsEnabled = false;
                    addButton.Visibility = Visibility.Collapsed;

                    BtDelAcc.IsEnabled = false;
                    BtDelAcc.Visibility = Visibility.Collapsed;

                    userList.IsEnabled = false;
                    userList.Visibility = Visibility.Collapsed;

                    uploadImage.IsEnabled = false;
                    uploadImage.Visibility = Visibility.Collapsed;

                    listUsers.IsEnabled = false;
                    listUsers.Visibility = Visibility.Collapsed;

                    //заранее извиняюсь за вульгарные название, я просто пипец как спать хочу, потом поменяю
                    Cum.IsEnabled = false;
                    Cum.Visibility = Visibility.Collapsed;
                    
                    break;
                
                default:
                    MessageBox.Show("Произошла какае-то ощибка с данными пользователя. Вас перекинут на страницу авторизации.", "О-оу", MessageBoxButton.OK, MessageBoxImage.Error);
                    AppFrame.frameMain.Navigate(new LoginPage());
                    break;
            }

            userRole.Items.Add("");

            for (int i = 0; i < AppConnect.model1db.Roles_ToyStore.ToList().Count; i++)
            {
                userRole.Items.Add(AppConnect.model1db.Roles_ToyStore.ToList()[i]);
            }

            DataContext = _curUser;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new ShopPage(use));
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
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

        private void BtExit_Click(object sender, RoutedEventArgs e)
        {
            var exitBi = MessageBox.Show("Вы уверенны, что хотите закрыть приложение?", "Выход из приложения", MessageBoxButton.YesNo, MessageBoxImage.Hand);

            if (exitBi == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void BtDelAcc_Click(object sender, RoutedEventArgs e)
        {
            var delOne = MessageBox.Show("Вы уверенны что хотите удалить свой аакаунт?", "Удаление аккаунта", MessageBoxButton.YesNo, MessageBoxImage.Hand);

            if (delOne == MessageBoxResult.Yes)
            {
                var delTwo = MessageBox.Show("Вы ТОЧНО уверенны, что хотите удалить свой аккаунт? Это безвозратно.", "Удаление аккаунта", MessageBoxButton.YesNo, MessageBoxImage.Hand);

                if (delTwo == MessageBoxResult.Yes)
                {
                    var delThree = MessageBox.Show("Вы ТОЧНО-ТОЧНО уверенны, что хотите удалить свой аккаунт? Это последнее окно, которое отговаривает вас от этого.", "Удаление аккаунта", MessageBoxButton.YesNo, MessageBoxImage.Hand);

                    if (delThree == MessageBoxResult.Yes)
                    {
                        //удаление аккаунта
                    }
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите добавить этого пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.model1db.Users_ToyStore.Add(_curUser);
                    AppConnect.model1db.SaveChanges();
                    MessageBox.Show("Данные успешно добавленны", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите изменить параметры этого пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.model1db.SaveChanges();
                    MessageBox.Show("Данные успешно редактированы", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void userList_Click(object sender, RoutedEventArgs e)
        {
            listUsers.IsEnabled = true;
            listUsers.Visibility = Visibility.Visible;
        }

        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userName.Text))
            {
                userName.Visibility = Visibility.Collapsed;
                userNamePlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void userLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userLogin.Text))
            {
                userLogin.Visibility = Visibility.Collapsed;
                userLoginPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void userPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userPassword.Text))
            {
                userPassword.Visibility = Visibility.Collapsed;
                userPasswordPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void userPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userPhone.Text))
            {
                userPhone.Visibility = Visibility.Collapsed;
                userPhonePlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void userMail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userMail.Text))
            {
                userMail.Visibility = Visibility.Collapsed;
                userMailPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void userNamePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            userNamePlaceHolder.Visibility = Visibility.Collapsed;
            userName.Visibility = Visibility.Visible;
            userName.Focus();
        }

        private void userLoginPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            userLoginPlaceHolder.Visibility = Visibility.Collapsed;
            userLogin.Visibility = Visibility.Visible;
            userLogin.Focus();
        }

        private void userPasswordPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            userPasswordPlaceHolder.Visibility = Visibility.Collapsed;
            userPassword.Visibility = Visibility.Visible;
            userPassword.Focus();
        }

        private void userPhonePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            userPhonePlaceHolder.Visibility = Visibility.Collapsed;
            userPhone.Visibility = Visibility.Visible;
            userPhone.Focus();
        }

        private void userMailPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            userMailPlaceHolder.Visibility = Visibility.Collapsed;
            userMail.Visibility = Visibility.Visible;
            userMail.Focus();
        }

        private void uploadImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? responce = openFileDialog.ShowDialog();

            if(responce == true)
            {
                var uni = MessageBox.Show("Вы хотите загрузить это изображение?", "Уведомление", MessageBoxButton.YesNo);
                if (uni == MessageBoxResult.Yes)
                {
                    //загрузка изображения
                    string soup = openFileDialog.FileName;
                    FileInfo inf = new FileInfo(soup);
                    string d = @"/images/" + System.IO.Path.GetFileName(soup);
                    inf.CopyTo(d);
                }
            }

        }
    }
}
