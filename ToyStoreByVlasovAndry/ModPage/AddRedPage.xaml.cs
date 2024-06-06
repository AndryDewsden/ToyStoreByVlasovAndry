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

namespace ToyStoreByVlasovAndry.ModPage
{
    public partial class AddRedPage : Page
    {
        private Toys_ToyStore _curToy = new Toys_ToyStore();
        Users_ToyStore use = new Users_ToyStore();
        public AddRedPage(Toys_ToyStore toy, Users_ToyStore user)
        {
            InitializeComponent();
            use = user;

            Combo_names.Items.Add("-выбрать-");
            Combo_maker.Items.Add("-выбрать-");
            Combo_giver.Items.Add("-выбрать-");
            Combo_ed.Items.Add("-выбрать-");
            Combo_cat.Items.Add("-выбрать-");

            //заполнение списка категорий
            for (int i = 0; i < AppConnect.model1db.Categories_ToyStore.ToList().Count; i++)
            {
                Combo_names.Items.Add(AppConnect.model1db.Categories_ToyStore.ToList()[i]);
            }

            //заполнение списка стран
            for (int i = 0; i < AppConnect.model1db.Countries_ToyStore.ToList().Count; i++)
            {
                Combo_maker.Items.Add(AppConnect.model1db.Countries_ToyStore.ToList()[i]);
            }

            //заполнение списка производителей
            for (int i = 0; i < AppConnect.model1db.Manufacturers_ToyStore.ToList().Count; i++)
            {
                Combo_giver.Items.Add(AppConnect.model1db.Manufacturers_ToyStore.ToList()[i]);
            }

            //заполнение списка поставщиков
            for (int i = 0; i < AppConnect.model1db.Providers_ToyStore.ToList().Count; i++)
            {
                Combo_ed.Items.Add(AppConnect.model1db.Providers_ToyStore.ToList()[i]);
            }

            //заполнение списка возрастных категорий
            for (int i = 0; i < AppConnect.model1db.AgeCategories_ToyStore.ToList().Count; i++)
            {
                Combo_cat.Items.Add(AppConnect.model1db.AgeCategories_ToyStore.ToList()[i]);
            }

            //Combo_names.ItemsSource = AppConnect.model1db.Names.ToList();
            //Combo_maker.ItemsSource = AppConnect.model1db.Makers.ToList();
            //Combo_giver.ItemsSource = AppConnect.model1db.Givers.ToList();
            //Combo_ed.ItemsSource = AppConnect.model1db.Edenices.ToList();
            //Combo_cat.ItemsSource = AppConnect.model1db.Categorys.ToList();

            if (toy != null)
            {
                _curToy = toy;
                Title = "Редактирование";
                Red.IsEnabled = true;
                
                check(article, articlePlaceHolder);
                check(prize, prizePlaceHolder);
                check(maxskid, maxskidPlaceHolder);
                check(kol, kolPlaceHolder);
                check(image, imagePlaceHolder);
            }
            else
            {
                Title = "Добавление";
                Red.IsEnabled = false;
                Red.Visibility = Visibility.Collapsed;
            }

            DataContext = _curToy;
        }

        //вернуться
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        //к магазину
        private void list_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new ShopPage(use));
        }

        //добавить товар
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Combo_names.SelectedIndex != 0 && Combo_maker.SelectedIndex != 0 && Combo_giver.SelectedIndex != 0 && Combo_ed.SelectedIndex != 0 && Combo_cat.SelectedIndex != 0 && article.Text != "" && prize.Text != "" && maxskid.Text != "" && kol.Text != "")
            {
                var res = MessageBox.Show("Вы действительно хотите добавить этот товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model1db.Toys_ToyStore.Add(_curToy);
                        AppConnect.model1db.SaveChanges();
                        MessageBox.Show("Данные успешно добавленны", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.frameMain.Navigate(new ShopPage(use));
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля!", "Уведомлениее", MessageBoxButton.OK);
            }
        }

        //редактировать товар
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (Combo_names.SelectedIndex != 0 && Combo_maker.SelectedIndex != 0 && Combo_giver.SelectedIndex != 0 && Combo_ed.SelectedIndex != 0 && Combo_cat.SelectedIndex != 0 && article.Text != "" && prize.Text != "" && maxskid.Text != "" && kol.Text != "")
            {
                var res = MessageBox.Show("Вы действительно хотите редактировать этот товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        
                        AppConnect.model1db.SaveChanges();
                        MessageBox.Show("Данные успешно редактированы", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.frameMain.Navigate(new ShopPage(use));
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля!", "Уведомлениее", MessageBoxButton.OK);
            }
        }

        private void article_LostFocus(object sender, RoutedEventArgs e)
        {
            check(article, articlePlaceHolder);
            placeHolder(article, articlePlaceHolder);
        }

        private void articlePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(article, articlePlaceHolder);
            article.Focus();
        }

        private void prize_LostFocus(object sender, RoutedEventArgs e)
        {
            check(prize, prizePlaceHolder);
            placeHolder(prize, prizePlaceHolder);
        }

        private void prizePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(prize, prizePlaceHolder);
            prize.Focus();
        }

        private void maxskid_LostFocus(object sender, RoutedEventArgs e)
        {
            check(maxskid, maxskidPlaceHolder);
            placeHolder(maxskid, maxskidPlaceHolder);
        }

        private void maxskidPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(maxskid, maxskidPlaceHolder);
            maxskid.Focus();
        }

        private void kol_LostFocus(object sender, RoutedEventArgs e)
        {
            check(kol, kolPlaceHolder);
            placeHolder(kol, kolPlaceHolder);
        }

        private void kolPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(kol, kolPlaceHolder);
            kol.Focus();
        }

        private void image_LostFocus(object sender, RoutedEventArgs e)
        {
            check(image, imagePlaceHolder);
            placeHolder(image, imagePlaceHolder);
        }

        private void imagePlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(image, imagePlaceHolder);
            image.Focus();
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

        private void check(TextBox org, TextBox place)
        {
            if (org.Text == null)
            {
                placeHolder(org, place);
            }
            else
            {
                Original(org, place);
            }
        }
    }
}
