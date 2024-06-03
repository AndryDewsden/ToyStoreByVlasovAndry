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
using ToyStoreByVlasovAndry.ModPage;

namespace ToyStoreByVlasovAndry.Content
{
    /// <summary>
    /// Логика взаимодействия для ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        Users_ToyStore use = new Users_ToyStore();
        public ShopPage(Users_ToyStore user)
        {
            InitializeComponent();
            use = user;

            userDisplay.Content = user.user_name;

            switch (user.user_id_role)
            {
                case 1:
                    //админ всё доступно
                    addConMenu.IsEnabled = true;
                    editConMenu.IsEnabled = true;
                    delConMenu.IsEnabled = true;

                    addButton.IsEnabled = true;
                    editButton.IsEnabled = true;
                    delButton.IsEnabled = true;
                    break;
                case 2:
                    //пользователь
                    addConMenu.IsEnabled = false;
                    editConMenu.IsEnabled = false;
                    delConMenu.IsEnabled = false;
                    addConMenu.Visibility = Visibility.Collapsed;
                    editConMenu.Visibility = Visibility.Collapsed;
                    delConMenu.Visibility = Visibility.Collapsed;

                    addButton.IsEnabled = false;
                    editButton.IsEnabled = false;
                    delButton.IsEnabled = false;
                    addButton.Visibility = Visibility.Collapsed;
                    editButton.Visibility = Visibility.Collapsed;
                    delButton.Visibility = Visibility.Collapsed;
                    break;
                default:
                    MessageBox.Show("Произошла какае-то ощибка с данными пользователя. Вас перекинут на страницу авторизации.", "О-оу", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            //сортировшик
            Sorter.ItemsSource = new Sort[]
            {
                new Sort { Name_sorter = "нет" },
                new Sort { Name_sorter = "к максимальной" },
                new Sort { Name_sorter = "к минимальной" }
            };
            Sorter.SelectedIndex = 0;

            //фильтр
            Filter.ItemsSource = new Filt[]
            {
                new Filt { Name_filter = "нет" },
                new Filt { Name_filter = "до 1000" },
                new Filt { Name_filter = "от 1000 до 2000" },
                new Filt { Name_filter = "от 2000" }
            };
            Filter.SelectedIndex = 0;

            //список
            listProducts.ItemsSource = FindProduct();
        }

        public class Sort
        {
            public string Name_sorter { get; set; } = "";
            public override string ToString() => $"{Name_sorter}";
        }

        public class Filt
        {
            public string Name_filter { get; set; } = "";
            public override string ToString() => $"{Name_filter}";
        }

        //составление списка
        Toys_ToyStore[] FindProduct()
        {
            var products = AppConnect.model1db.Toys_ToyStore.ToList();
            var producttall = products;

            if (Searcher.Text != null)
            {
                products = products.Where(x => x.toy_name.ToLower().Contains(Searcher.Text.ToLower())).ToList();
            }

            if (Filter.SelectedIndex > 0)
            {
                switch (Filter.SelectedIndex)
                {
                    case 1:
                        products = products.Where(x => x.toy_retailPrice > 0 && x.toy_retailPrice < 1000).ToList();
                        break;
                    case 2:
                        products = products.Where(x => x.toy_retailPrice >= 1000 && x.toy_retailPrice < 2000).ToList();
                        break;
                    case 3:
                        products = products.Where(x => x.toy_retailPrice >= 2000).ToList();
                        break;
                }
            }

            if (Sorter.SelectedIndex > 0)
            {
                switch (Sorter.SelectedIndex)
                {
                    case 1:
                        products = products.OrderBy(x => x.toy_wholesalePrice).ToList();
                        break;
                    case 2:
                        products = products.OrderByDescending(x => x.toy_wholesalePrice).ToList();
                        break;
                }
            }

            if (products.Count > 0)
            {
                Counter.Text = $"Найдено {products.Count} из {producttall.Count} товаров.";
            }
            else
            {
                Counter.Text = "Ничего не найдено.";
            }

            return products.ToArray();
        }

        //кнопка возврашения назад
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        //обновление страницы
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProducts.ItemsSource = FindProduct();
        }

        //обновление страницы
        private void Sorter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProducts.ItemsSource = FindProduct();
        }

        //обновление страницы
        private void Searcher_TextChanged(object sender, TextChangedEventArgs e)
        {
            listProducts.ItemsSource = FindProduct();
        }

        //добавление товара через кнопку
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            addFun();
        }

        //редактирование товара через кнопку
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            editFun();
        }

        //удаление товара через кнопку
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            delFun();
        }

        //обновление контента на странице
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppConnect.model1db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listProducts.ItemsSource = AppConnect.model1db.Toys_ToyStore.ToList();
            }
        }

        //перевод товара в корзину через контекстное меню
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            AddToCart();
        }

        //перейти в корзину
        private void goCart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new CartPage(use));
        }

        //перейти в аккаунт
        private void userDisplay_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new UserPage(use));
        }

        //добавить товар через админа
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addFun();
        }

        //редактировать товар через админа
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Toys_ToyStore)listProducts.SelectedItem != null)
            {
                editFun();
            }
        }

        //удаление товара через админа
        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Toys_ToyStore)listProducts.SelectedItem != null)
            {
                delFun();
            }
        }

        //Переход на добавление товара через контекстное меню через админа
        private void addFun()
        {
            AppFrame.frameMain.Navigate(new AddRedPage(null, use));
        }

        //Переход на редактирование товара через контекстное меню через админа
        private void editFun()
        {
            Toys_ToyStore red = (Toys_ToyStore)listProducts.SelectedItem;
            AppFrame.frameMain.Navigate(new AddRedPage(red, use));
        }

        //Удаление товара через контекстное меню через админа
        private void delFun()
        {
            var del = (Toys_ToyStore)listProducts.SelectedItem;
            var res = MessageBox.Show($"Вы действительно хотите удалить этот товар?\n Будет удалён:\nНаименование: {del.toy_name} \nАртикль: {del.toy_discription} \n{del.pic}", "Уведомление", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (res == MessageBoxResult.OK)
            {
                try
                {
                    AppConnect.model1db.Toys_ToyStore.Remove(del);
                    AppConnect.model1db.SaveChanges();
                    listProducts.ItemsSource = FindProduct();
                    //MessageBox.Show("Данные успешно удалены", "Тестирование", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //кнопка на товаре
        private void Add_toCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"", "Тестирование", MessageBoxButton.OK);
            AddToCart();
        }

        //Добавление товара в заказ
        private void AddToCart()
        {
            string numOrder;
            Directories_ToyStore userList = AppConnect.model1db.Directories_ToyStore.FirstOrDefault(x => x.directory_id_user == use.id_user);
            
            //присваивание товара к номеру заказа
            if(userList == null)
            {
                //генератор номера заказа
                //numOrder = use.id_user.ToString();
                
                Random r = new Random();

                numOrder = "";

                while (AppConnect.model1db.Directories_ToyStore.Where(x => x.directory_order_number == numOrder).Count() > 0 || numOrder == "")
                {
                    numOrder = "R0";
                    for (int i = 0; i < 10; i++)
                    {
                        int t = r.Next(0, 2);
                        switch (t)
                        {
                            case 0:
                                //numOrder += Convert.ToChar(r.Next(97, 122));
                                numOrder += Convert.ToChar(r.Next(65, 90));
                                break;
                            case 1:
                                numOrder += r.Next(0, 10).ToString();
                                break;
                        }
                    }
                    if(AppConnect.model1db.Directories_ToyStore.Where(x => x.directory_order_number == numOrder).Count() > 0)
                    {
                        MessageBox.Show("Такой номер уже есть.", "lol", MessageBoxButton.OK);
                    }
                }


                try
                {
                    Directories_ToyStore userDir = new Directories_ToyStore()
                    {
                        directory_id_user = use.id_user,
                        directory_order_number = numOrder
                    };
                    AppConnect.model1db.Directories_ToyStore.Add(userDir);
                    AppConnect.model1db.SaveChanges();
                    //MessageBox.Show($"Новый номер сгенернирован: {numOrder}", "Тестирование", MessageBoxButton.OK);
                }
                catch
                {
                    MessageBox.Show("Ошибка при внедрении данных на сервер!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                //MessageBox.Show("Товару успешно присвоен номер", "Тестирование", MessageBoxButton.OK);
                numOrder = userList.directory_order_number.ToString();
            }

            //Добавление товара в корзину
            var ord = (Toys_ToyStore)listProducts.SelectedItem;

            var goodOrder = AppConnect.model1db.Orders_ToyStore.FirstOrDefault(x => x.order_id_toy == ord.id_toy && x.order_number == numOrder);

            if (ord != null && goodOrder == null)
            {
                try
                {
                    Orders_ToyStore userOrder = new Orders_ToyStore()
                    {
                        order_number = numOrder,
                        order_id_toy = ord.id_toy,
                        order_quantity = 1
                    };
                    AppConnect.model1db.Orders_ToyStore.Add(userOrder);
                    AppConnect.model1db.SaveChanges();
                    //MessageBox.Show("Ваш товар успешно добавлен в корзину.", "Тестовое уведомление", MessageBoxButton.OK);
                }
                catch
                {
                    MessageBox.Show("Ошибка при внедрении данных на сервер!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    goodOrder.order_quantity = goodOrder.order_quantity + 1;
                    AppConnect.model1db.SaveChanges();
                    //MessageBox.Show("Данные успешно редактированы", "Тестирование", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //MessageBox.Show("Этот товар уже есть в вашей корзине", "Тестовое уведомление", MessageBoxButton.OK);
            }
        }

        private void Add_toCart_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }

        private void Searcher_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Searcher.Text))
            {
                Searcher.Visibility = Visibility.Collapsed;
                SearcherPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void SearcherPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            SearcherPlaceHolder.Visibility = Visibility.Collapsed;
            Searcher.Visibility = Visibility.Visible;
            Searcher.Focus();
        }
    }
}
