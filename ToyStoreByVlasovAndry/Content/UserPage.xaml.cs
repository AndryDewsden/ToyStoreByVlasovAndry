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

            WindowShow(listUsers, 2);
            WindowShow(userdactor, 2);
            WindowShow(listOrder, 2);
            WindowShow(ordersdactor, 2);

            switch (user.user_id_role)
            {
                case 1:
                    Title = $"Страница администратора {user.user_name}";

                    WindowShow(addButton, 1);
                    WindowShow(BtDelAcc, 2);
                    WindowShow(uploadImage, 1);

                    userRole.Items.Add("--выбрать--");

                    for (int i = 0; i < AppConnect.model1db.Roles_ToyStore.ToList().Count; i++)
                    {
                        userRole.Items.Add(AppConnect.model1db.Roles_ToyStore.ToList()[i]);
                    }

                    ComboMenu.Items.Add("Пользователи");
                    ComboMenu.Items.Add("Заказы");

                    orderStatus.Items.Add("");

                    for (int i = 0; i < AppConnect.model1db.Status_ToyStore.ToList().Count; i++)
                    {
                        orderStatus.Items.Add(AppConnect.model1db.Status_ToyStore.ToList()[i]);
                    }

                    break;

                case 2:
                    Title = $"Страница пользователя {user.user_name}";

                    WindowShow(addButton, 2);
                    WindowShow(BtDelAcc, 2);
                    WindowShow(listUsers, 2);
                    WindowShow(uploadImage, 2);
                    WindowShow(ComboMenu, 2);
                    break;
                
                case 3:
                    Title = $"Страница менеджера {user.user_name}";

                    WindowShow(addButton, 1);
                    WindowShow(BtDelAcc, 2);
                    WindowShow(uploadImage, 1);

                    userRole.Items.Add("--выбрать--");

                    for (int i = 0; i < AppConnect.model1db.Roles_ToyStore.ToList().Count; i++)
                    {
                        if (AppConnect.model1db.Roles_ToyStore.ToList()[i].id_role != 1 && AppConnect.model1db.Roles_ToyStore.ToList()[i].id_role != 3)
                        {
                            userRole.Items.Add(AppConnect.model1db.Roles_ToyStore.ToList()[i]);
                        }
                        else
                        {
                            userRole.Items.Remove(AppConnect.model1db.Roles_ToyStore.ToList()[i]);
                        }
                    }

                    ComboMenu.Items.Add("Пользователи");
                    ComboMenu.Items.Add("Заказы");

                    orderStatus.Items.Add("");

                    for (int i = 0; i < AppConnect.model1db.Status_ToyStore.ToList().Count; i++)
                    {

                        orderStatus.Items.Add(AppConnect.model1db.Status_ToyStore.ToList()[i]);
                    }
                    break;

                default:
                    MessageBox.Show("Произошла какае-то ощибка с данными пользователя. Вас перекинут на страницу авторизации.", "О-оу", MessageBoxButton.OK, MessageBoxImage.Error);
                    AppFrame.frameMain.Navigate(new LoginPage());
                    break;
            }

            listUsers.ItemsSource = fillUsers();
            listOrder.ItemsSource = fillOrders();
        }

        Users_ToyStore[] fillUsers()
        {
            switch (use.user_id_role)
            {
                case 1:
                    var pro = AppConnect.model1db.Users_ToyStore.Where(x => x.id_user != use.id_user && x.user_id_role != 1).ToList();
                    return pro.ToArray();
                case 3:
                    pro = AppConnect.model1db.Users_ToyStore.Where(x => x.id_user != use.id_user && x.user_id_role != 1 && x.user_id_role != 3).ToList();
                    return pro.ToArray();
                default:
                    pro = AppConnect.model1db.Users_ToyStore.Where(x => x.user_id_role != 1 && x.user_id_role != 2 && x.user_id_role != 3).ToList();
                    return pro.ToArray();
            }
        }

        Directories_ToyStore[] fillOrders()
        {
            var pro = AppConnect.model1db.Directories_ToyStore.ToList();
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
            if (userName.Text != "" && userLogin.Text != "" && userPassword.Text != "" && userRole.SelectedIndex != 0)
            {
                var res = MessageBox.Show("Вы действительно хотите добавить этого пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        _curUser.user_name = userName.Text;
                        _curUser.user_login = userLogin.Text;
                        _curUser.user_password = userPassword.Text;
                        _curUser.user_id_role = userRole.SelectedIndex;

                        if (userPhone.Text == "")
                        {
                            _curUser.user_phone = null;
                        }
                        else
                        {
                            _curUser.user_phone = userPhone.Text;
                        }

                        if (userMail.Text == "")
                        {
                            _curUser.user_mail = null;
                        }
                        else
                        {
                            _curUser.user_mail = userMail.Text;
                        }

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
            else
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK);
            }
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            if (userName.Text != "" && userLogin.Text != "" && userPassword.Text != "" && userRole.SelectedIndex != 0)
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

                        if (userPhone.Text == "")
                        {
                            _curUser.user_phone = null;
                        }
                        else
                        {
                            _curUser.user_phone = userPhone.Text;
                        }

                        if (userMail.Text == "")
                        {
                            _curUser.user_mail = null;
                        }
                        else
                        {
                            _curUser.user_mail = userMail.Text;
                        }

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
            else
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK);
            }
        }

        private void userList_Click(object sender, RoutedEventArgs e)
        {
            listUsers.IsEnabled = true;
            listUsers.Visibility = Visibility.Visible;
        }

        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(userName, userNamePlaceHolder, 2);
        }

        private void userLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(userLogin, userLoginPlaceHolder, 2);
        }

        private void userPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(userPassword, userPasswordPlaceHolder, 2);
        }

        private void userPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(userPhone, userPhonePlaceHolder, 2);
        }

        private void userMail_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(userMail, userMailPlaceHolder, 2);
        }

        private void userNamePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userName, userNamePlaceHolder, 1);
            userName.Focus();
        }

        private void userLoginPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userLogin, userLoginPlaceHolder, 1);
            userLogin.Focus();
        }

        private void userPasswordPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userPassword, userPasswordPlaceHolder, 1);
            userPassword.Focus();
        }

        private void userPhonePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userPhone, userPhonePlaceHolder, 1);
            userPhone.Focus();
        }

        private void userMailPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(userMail, userMailPlaceHolder, 1);
            userMail.Focus();
        }

        private void Original(TextBox org, TextBox place, int r)
        {
            switch (r)
            {
                case 1:
                    place.Visibility = Visibility.Collapsed;
                    org.Visibility = Visibility.Visible;
                    break;
                case 2:
                    if (string.IsNullOrEmpty(org.Text))
                    {
                        org.Visibility = Visibility.Collapsed;
                        place.Visibility = Visibility.Visible;
                    }
                    break;
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

            Original(userName, userNamePlaceHolder, 1);
            Original(userLogin, userLoginPlaceHolder, 1);
            Original(userPassword, userPasswordPlaceHolder, 1);
            
            if (userPhone.Text != "")
            {
                Original(userPhone, userPhonePlaceHolder, 1);
            }
            else
            {
                Original(userPhone, userPhonePlaceHolder, 2);
            }
            if (userMail.Text != "")
            {
                Original(userMail, userMailPlaceHolder, 1);
            }
            else
            {
                Original(userMail, userMailPlaceHolder, 2);
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

                    Original(userName, userNamePlaceHolder, 2);
                    Original(userLogin, userLoginPlaceHolder, 2);
                    Original(userPassword, userPasswordPlaceHolder, 2);
                    Original(userPhone, userPhonePlaceHolder, 2);
                    Original(userMail, userMailPlaceHolder, 2);

                    listUsers.ItemsSource = fillUsers();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void WindowShow(ListView sim, int s)
        {
            switch (s)
            {
                case 1:
                    sim.IsEnabled = true;
                    sim.Visibility = Visibility.Visible;
                    break;
                case 2:
                    sim.IsEnabled = false;
                    sim.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void WindowShow(StackPanel sim, int s)
        {
            switch (s)
            {
                case 1:
                    sim.IsEnabled = true;
                    sim.Visibility = Visibility.Visible;
                    break;
                case 2:
                    sim.IsEnabled = false;
                    sim.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void WindowShow(Button sim, int s)
        {
            switch (s)
            {
                case 1:
                    sim.IsEnabled = true;
                    sim.Visibility = Visibility.Visible;
                    break;
                case 2:
                    sim.IsEnabled = false;
                    sim.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void WindowShow(ComboBox sim, int s)
        {
            switch (s)
            {
                case 1:
                    sim.IsEnabled = true;
                    sim.Visibility = Visibility.Visible;
                    break;
                case 2:
                    sim.IsEnabled = false;
                    sim.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void ComboMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboMenu.SelectedIndex)
            {
                case 0:
                    WindowShow(listUsers, 1);
                    WindowShow(userdactor, 1);
                    WindowShow(listOrder, 2);
                    WindowShow(ordersdactor, 2);
                    break;

                case 1:
                    WindowShow(listUsers, 2);
                    WindowShow(userdactor, 2);
                    WindowShow(listOrder, 1);
                    WindowShow(ordersdactor, 1);
                    break;

                default:
                    WindowShow(listUsers, 2);
                    WindowShow(userdactor, 2);
                    WindowShow(listOrder, 2);
                    WindowShow(ordersdactor, 2);
                    break;
            }
        }

        Directories_ToyStore _curOrder = new Directories_ToyStore();
        private void listOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _curOrder = (Directories_ToyStore)listOrder.SelectedItem;
            usl.Content = AppConnect.model1db.Users_ToyStore.FirstOrDefault(x => x.id_user == _curOrder.directory_id_user).user_name;
            numl.Content = _curOrder.directory_order_number;

            orderStatus.SelectedItem = AppConnect.model1db.Status_ToyStore.FirstOrDefault(x => x.id_status == _curOrder.directory_status);
        }

        private void redOrder_Click(object sender, RoutedEventArgs e)
        {
            if (orderStatus.SelectedIndex != 0)
            {
                var res = MessageBox.Show("Вы действительно хотите изменить статус этого заказа?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        _curOrder.directory_status = orderStatus.SelectedIndex;
                        DataContext = _curOrder;

                        AppConnect.model1db.SaveChanges();
                        listOrder.ItemsSource = fillOrders();
                        //MessageBox.Show("Данные успешно редактированы", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK);
            }
}

        private void delOrder_Click(object sender, RoutedEventArgs e)
        {
            var del = (Directories_ToyStore)listOrder.SelectedItem;
            var res = MessageBox.Show($"Вы действительно хотите удалить этот заказ?\nВсе товары этого заказа также будут удалены.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    int f = AppConnect.model1db.Orders_ToyStore.Where(x => x.order_id_directory == del.id_directory).Count();

                    for(int i = 0; i < f; i++)
                    {
                        var c = AppConnect.model1db.Orders_ToyStore.FirstOrDefault(x => x.order_id_directory == del.id_directory);
                        AppConnect.model1db.Orders_ToyStore.Remove(c);
                    }
                    AppConnect.model1db.Directories_ToyStore.Remove(del);
                    AppConnect.model1db.SaveChanges();

                    listOrder.ItemsSource = fillUsers();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
