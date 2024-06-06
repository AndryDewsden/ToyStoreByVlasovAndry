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
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Paragraph = iTextSharp.text.Paragraph;
using Aspose.BarCode.Generation;
using Image = iTextSharp.text.Image;
using System.Windows.Markup;

namespace ToyStoreByVlasovAndry.Content
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        Users_ToyStore use = new Users_ToyStore();
        string numOrder = null;
        public Order(Users_ToyStore user)
        {
            InitializeComponent();

            use = user;
            userDisplay.Content = use.user_name;

            Directories_ToyStore userList = AppConnect.model1db.Directories_ToyStore.FirstOrDefault(x => x.directory_id_user == use.id_user && x.directory_status != 2);

            if (userList != null)
            {
                numOrder = userList.directory_order_number;
                listCart.ItemsSource = FillCart();
            }
            OrderN.Content = numOrder;
        }

        Orders_ToyStore[] FillCart()
        {
            var productsInCart = AppConnect.model1db.Orders_ToyStore.ToList();

            if (numOrder != null)
            {
                //
                var num = AppConnect.model1db.Directories_ToyStore.FirstOrDefault(x => x.directory_order_number == numOrder).id_directory;
                productsInCart = AppConnect.model1db.Orders_ToyStore.Where(x => x.order_id_directory == num).ToList();
            }

            int CountGood = 0;
            int WholeSaleSum = 0;
            int RetailSaleSum = 0;

            for(int i = 0; i < productsInCart.Count; i++)
            {
                int quan = productsInCart[i].order_quantity;
                int toy_id = productsInCart[i].order_id_toy;
                Toys_ToyStore pro = AppConnect.model1db.Toys_ToyStore.FirstOrDefault(x => x.id_toy == toy_id);
                int whol = pro.toy_wholesalePrice;
                int ret = pro.toy_retailPrice;

                CountGood += quan;
                WholeSaleSum += quan * whol;
                RetailSaleSum += quan * ret;
            }

            GoodCount.Content = CountGood;
            WholeSale.Content = WholeSaleSum;
            Retail.Content = RetailSaleSum;
            
            return productsInCart.ToArray();
        }

        private void goCart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new CartPage(use));
        }

        private void userDisplay_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new UserPage(use));
        }

        private void doDoc_Click(object sender, RoutedEventArgs e)
        {
            doPDF();
        }

        private void doBarCode_Click(object sender, RoutedEventArgs e)
        {
            QRimg.Source = doQR();
        }

        private void doPDF()
        {
            //новый документ
            Document doc = new Document();

            try
            {
                PdfWriter.GetInstance(doc, new FileStream("..\\..\\Check" + use.user_name + "_" + numOrder +".pdf", FileMode.Create));

                doc.Open();
                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Font font = new Font(baseFont, 12);
                Font font1 = new Font(baseFont, 23, 3, BaseColor.BLACK);

                Paragraph line = new Paragraph("--------------------------------------------------------------------", font1);
                line.Alignment = Element.ALIGN_CENTER;
                doc.Add(line);

                Paragraph title = new Paragraph("ЧЕК", font1);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(line);

                doQR();
                Image Qimg = Image.GetInstance(@"C:\Users\10210795\source\repos\ToyStoreByVlasovAndry\ToyStoreByVlasovAndry\UserData\" + b.ToString() + "_QR_" + use.user_name + "_" + numOrder + ".png");

                Qimg.ScaleAbsolute(200f, 200f);
                Qimg.Alignment = Element.ALIGN_CENTER;
                doc.Add(Qimg);
                
                doc.Add(line);
                
                int cnt = 0;
                decimal sumW = 0;
                decimal sumR = 0;

                foreach (var item in listCart.Items)
                {
                    if (item is Orders_ToyStore)
                    {
                        Orders_ToyStore data = (Orders_ToyStore)item;

                        Image img = Image.GetInstance(@"C:\Users\10210795\source\repos\ToyStoreByVlasovAndry\ToyStoreByVlasovAndry\images\" + data.Toys_ToyStore.toy_image);
                        img.ScaleAbsolute(100f, 100f);
                        doc.Add(img);
                        doc.Add(new Paragraph($"Наименование: {data.Toys_ToyStore.toy_name}", font));
                        doc.Add(new Paragraph($"Оптовая цена: {data.Toys_ToyStore.toy_wholesalePrice}", font));
                        doc.Add(new Paragraph($"Розничная цена: {data.Toys_ToyStore.toy_retailPrice}", font));
                        doc.Add(new Paragraph($"Количество товара: {data.order_quantity}", font));
                        doc.Add(new Paragraph($"Итоговая оптовая цена: {data.Toys_ToyStore.toy_wholesalePrice * data.order_quantity}", font));
                        doc.Add(new Paragraph($"Итоговая розничная цена: {data.Toys_ToyStore.toy_retailPrice * data.order_quantity}", font));
                        doc.Add(line);

                        cnt += data.order_quantity;
                        sumW += data.Toys_ToyStore.toy_wholesalePrice * data.order_quantity;
                        sumR += data.Toys_ToyStore.toy_retailPrice * data.order_quantity;
                    }
                }

                Paragraph ordN = new Paragraph($"Номер заказа: {numOrder}", font);
                ordN.Alignment = Element.ALIGN_RIGHT;
                doc.Add(ordN);
                
                Paragraph userpar = new Paragraph($"Пользователь: {use.user_name}", font);
                Paragraph TotalQuantity = new Paragraph($"Общее количество товаров: {cnt}", font);
                Paragraph TotalSumW = new Paragraph($"Оптовая сумма: {sumW}", font);
                Paragraph TotalSumR = new Paragraph($"Розничная сумма: {sumR}", font);
                
                userpar.Alignment = Element.ALIGN_RIGHT;
                TotalQuantity.Alignment = Element.ALIGN_RIGHT;
                TotalSumW.Alignment = Element.ALIGN_RIGHT;
                TotalSumR.Alignment = Element.ALIGN_RIGHT;

                doc.Add(userpar);
                doc.Add(TotalQuantity);
                doc.Add(TotalSumW);
                doc.Add(TotalSumR);
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }
            finally
            {
                doc.Close();
            }
        }

        int a = 1;
        int b = 1;
        private BitmapImage doQR()
        {
            //
            BitmapImage bitmap = new BitmapImage();
            //
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://bom.firpo.ru/Public/86");
            //
            gen.Parameters.Barcode.XDimension.Pixels = 34;


            string dataDir = @"C:\Users\10210795\source\repos\ToyStoreByVlasovAndry\ToyStoreByVlasovAndry\UserData\";

            //string uuid = Guid.NewGuid().ToString();

            string li = a.ToString() + "_QR_" + use.user_name + "_" + numOrder + ".png";

            if (bitmap != null)
            {
                gen.Save(dataDir + li, BarCodeImageFormat.Png);
            }

            bitmap.BeginInit();
            
            bitmap.UriSource = new Uri(dataDir + li);

            bitmap.EndInit();

            //QRimg.Source = bitmap;
            b = a;
            a++;
            return bitmap;
        }

        Directories_ToyStore directories = new Directories_ToyStore();
        private void endOrder_Click(object sender, RoutedEventArgs e)
        {
            doPDF();

            var res = MessageBox.Show("Вы действительно хотите завершить этот заказ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    directories = AppConnect.model1db.Directories_ToyStore.FirstOrDefault(x => x.directory_order_number == numOrder);
                    directories.directory_status = 2;
                    AppConnect.model1db.SaveChanges();
                    MessageBox.Show("Ваш заказ успешно оформлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.frameMain.Navigate(new ShopPage(use));
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
