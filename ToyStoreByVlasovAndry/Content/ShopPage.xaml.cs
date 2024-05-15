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

        Toys_ToyStore[] FindProduct()
        {
            //составление списка
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
                Counter.Text = "Найдено " + products.Count + " из " + producttall.Count + " товаров.";
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

        //перевод товара в корзину
        private void Buy_Click(object sender, RoutedEventArgs e)
        {

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

        //добавить товар
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addFun();
        }

        //редактировать товар
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Main)listProducts.SelectedItem != null)
            {
                editFun();
            }
        }

        //удаление товара
        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Main)listProducts.SelectedItem != null)
            {
                delFun();
            }
        }

        //Переход на добавление товара через контекстное меню
        private void addFun()
        {
            AppFrame.frameMain.Navigate(new AddRedPage(null, use));
        }

        //Переход на редактирование товара через контекстное меню
        private void editFun()
        {
            Toys_ToyStore red = (Toys_ToyStore)listProducts.SelectedItem;
            AppFrame.frameMain.Navigate(new AddRedPage(red, use));
        }

        //Удаление товара через контекстное меню
        private void delFun()
        {
            var del = (Toys_ToyStore)listProducts.SelectedItem;
            var res = MessageBox.Show($"Вы действительно хотите удалить этот товар?\n Будет удалён:\nНаименование: {del.toy_name} \nАртикль: {del.toy_discription} \n{del.toy_image}", "Уведомление", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (res == MessageBoxResult.OK)
            {
                try
                {
                    AppConnect.model1db.Toys_ToyStore.Remove(del);
                    AppConnect.model1db.SaveChanges();
                    listProducts.ItemsSource = FindProduct();
                    MessageBox.Show("Данные успешно удалены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Add_toCart_Click(object sender, EventArgs e)
        {
            int user = use.id_user;
            int good = 1;
            int count = 1;

            MessageBox.Show("АЙДИ товара: " + good + "\nАЙДИ пользователя: " + user, "1", MessageBoxButton.OK);
        }
    }
}
