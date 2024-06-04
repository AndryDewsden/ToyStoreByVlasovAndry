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

                    listUsers.IsEnabled = false;
                    listUsers.Visibility = Visibility.Collapsed;

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

            userRole.Items.Add("--выбрать--");

            for (int i = 0; i < AppConnect.model1db.Roles_ToyStore.ToList().Count; i++)
            {
                userRole.Items.Add(AppConnect.model1db.Roles_ToyStore.ToList()[i]);
            }

            listUsers.ItemsSource = fillUsers();
        }

        Users_ToyStore[] fillUsers()
        {
            var pro = AppConnect.model1db.Users_ToyStore.Where(x => x.id_user != use.id_user).ToList();
            return pro.ToArray();
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
                    listUsers.ItemsSource = fillUsers();
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
                    _curUser.user_name = userName.Text;
                    _curUser.user_login = userLogin.Text;
                    _curUser.user_password = userPassword.Text;
                    _curUser.user_id_role = userRole.SelectedIndex;
                    _curUser.user_phone = userPhone.Text;
                    _curUser.user_mail = userMail.Text;

                    DataContext = _curUser;
                    
                    AppConnect.model1db.SaveChanges();
                    listUsers.ItemsSource = fillUsers();
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
            placeHolder(userName, userNamePlaceHolder);
        }

        private void userLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            placeHolder(userLogin, userLoginPlaceHolder);
        }

        private void userPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            placeHolder(userPassword, userPasswordPlaceHolder);
        }

        private void userPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            placeHolder(userPhone, userPhonePlaceHolder);
        }

        private void userMail_LostFocus(object sender, RoutedEventArgs e)
        {
            placeHolder(userMail, userMailPlaceHolder);
        }

        private void userNamePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userName, userNamePlaceHolder);
            userName.Focus();
        }

        private void userLoginPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userLogin, userLoginPlaceHolder);
            userLogin.Focus();
        }

        private void userPasswordPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userPassword, userPasswordPlaceHolder);
            userPassword.Focus();
        }

        private void userPhonePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userPhone, userPhonePlaceHolder);
            userPhone.Focus();
        }

        private void userMailPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userMail, userMailPlaceHolder);
            userMail.Focus();
        }

        private void Original(TextBox org, TextBox place)
        {
                place.Visibility = Visibility.Collapsed;
                org.Visibility = Visibility.Visible;
        }

        private void placeHolder(TextBox org, TextBox place)
        {
            if (string.IsNullOrEmpty(org.Text))
            {
                org.Visibility = Visibility.Collapsed;
                place.Visibility = Visibility.Visible;
            }
        }

        private void uploadImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? responce = openFileDialog.ShowDialog();

            if(responce == true)
            {
                var uni = MessageBox.Show("Вы хотите загрузить это изображение для товара?", "Уведомление", MessageBoxButton.YesNo);
                if (uni == MessageBoxResult.Yes)
                {
                    //загрузка изображения
                    string soup = openFileDialog.FileName;
                    FileInfo inf = new FileInfo(soup);
                    string d = @"C:\Users\10210795\source\repos\ToyStoreByVlasovAndry\ToyStoreByVlasovAndry\images\" + System.IO.Path.GetFileName(soup);
                    inf.CopyTo(d);
                }
            }

        }

        Users_ToyStore _curUser = new Users_ToyStore();
        private void listUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _curUser = (Users_ToyStore)listUsers.SelectedItem;
            
            userName.Text = _curUser.user_name;
            userLogin.Text = _curUser.user_login;
            userPassword.Text = _curUser.user_password;
            userRole.SelectedIndex = _curUser.user_id_role;
            userPhone.Text = _curUser.user_phone;
            userMail.Text = _curUser.user_mail;

            Original(userName, userNamePlaceHolder);
            Original(userLogin, userLoginPlaceHolder);
            Original(userPassword, userPasswordPlaceHolder);
            
            if (userPhone.Text != "")
            {
                Original(userPhone, userPhonePlaceHolder);
            }
            else
            {
                placeHolder(userPhone, userPhonePlaceHolder);
            }
            if (userMail.Text != "")
            {
                Original(userMail, userMailPlaceHolder);
            }
            else
            {
                placeHolder(userMail, userMailPlaceHolder);
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            var del = (Users_ToyStore)listUsers.SelectedItem;
            var res = MessageBox.Show($"Вы действительно хотите удалить этого пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.model1db.Users_ToyStore.Remove(del);
                    AppConnect.model1db.SaveChanges();

                    userName.Text = "";
                    userLogin.Text = "";
                    userPassword.Text = "";
                    userRole.SelectedIndex = 0;
                    userPhone.Text = "";
                    userMail.Text = "";

                    placeHolder(userName, userNamePlaceHolder);
                    placeHolder(userLogin, userLoginPlaceHolder);
                    placeHolder(userPassword, userPasswordPlaceHolder);
                    placeHolder(userPhone, userPhonePlaceHolder);
                    placeHolder(userMail, userMailPlaceHolder);

                    listUsers.ItemsSource = fillUsers();
                    //MessageBox.Show("Данные успешно удалены", "Тестирование", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
