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
    /// <summary>
    /// Логика взаимодействия для AddRedPage.xaml
    /// </summary>
    public partial class AddRedPage : Page
    {
        private Toys_ToyStore _curToy = new Toys_ToyStore();
        Users_ToyStore use = new Users_ToyStore();
        public AddRedPage(Toys_ToyStore toy, Users_ToyStore user)
        {
            InitializeComponent();
            use = user;

            Combo_names.Items.Add(" ");
            Combo_maker.Items.Add(" ");
            Combo_giver.Items.Add(" ");
            Combo_ed.Items.Add(" ");
            Combo_cat.Items.Add(" ");

            //категория
            for (int i = 0; i < AppConnect.model1db.Categories_ToyStore.ToList().Count; i++)
            {
                Combo_names.Items.Add(AppConnect.model1db.Categories_ToyStore.ToList()[i]);
            }

            //страна
            for (int i = 0; i < AppConnect.model1db.Countries_ToyStore.ToList().Count; i++)
            {
                Combo_maker.Items.Add(AppConnect.model1db.Countries_ToyStore.ToList()[i]);
            }

            //производитель
            for (int i = 0; i < AppConnect.model1db.Manufacturers_ToyStore.ToList().Count; i++)
            {
                Combo_giver.Items.Add(AppConnect.model1db.Manufacturers_ToyStore.ToList()[i]);
            }

            //поставщик
            for (int i = 0; i < AppConnect.model1db.Providers_ToyStore.ToList().Count; i++)
            {
                Combo_ed.Items.Add(AppConnect.model1db.Providers_ToyStore.ToList()[i]);
            }

            //возрастная категория
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
            var res = MessageBox.Show("Вы действительно хотите добавить этот товар?", "Уведомление", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (res == MessageBoxResult.OK)
            {
                try
                {
                    AppConnect.model1db.Toys_ToyStore.Add(_curToy);
                    AppConnect.model1db.SaveChanges();
                    MessageBox.Show("Данные успешно добавленны", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //редактировать товар
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите редактировать этот товар?", "Уведомление", MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (res == MessageBoxResult.OK)
            {
                try
                {
                    AppConnect.model1db.SaveChanges();
                    MessageBox.Show("Данные успешно редактированы", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
