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

            Directories_ToyStore userList = AppConnect.model1db.Directories_ToyStore.FirstOrDefault(x => x.directory_id_user == use.id_user);

            if (userList != null)
            {
                numOrder = userList.directory_order_number;
                listCart.ItemsSource = FillCart();
            }
        }

        Orders_ToyStore[] FillCart()
        {
            var productsInCart = AppConnect.model1db.Orders_ToyStore.ToList();

            if (numOrder != null)
            {
                productsInCart = AppConnect.model1db.Orders_ToyStore.Where(x => x.order_number == numOrder).ToList();
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
            AppFrame.frameMain.GoBack();
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
            doQR();
        }

        private void doPDF()
        {
            //новый документ
            Document doc = new Document();

            try
            {
                PdfWriter.GetInstance(doc, new FileStream("..\\..\\output.pdf", FileMode.Create));

                doc.Open();
                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Font font = new Font(baseFont, 12);
                Font font1 = new Font(baseFont, 23, 3, BaseColor.BLUE);
                Paragraph paragraph1 = new Paragraph("СПИСОК ТОВАРОВ ", font1);
                paragraph1.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph1);
                decimal sum = 0;

                foreach (var item in AppConnect.model1db.Toys_ToyStore.ToList())
                {
                    if (item is Toys_ToyStore)
                    {
                        Toys_ToyStore data = (Toys_ToyStore)item;

                        doc.Add(new Paragraph("Наименование: " + data.toy_name, font));
                        sum += data.toy_wholesalePrice;
                    }
                }
                Paragraph paragraph = new Paragraph("Сумма: " + sum.ToString(), font);
                paragraph.Alignment = Element.ALIGN_RIGHT;
                doc.Add(paragraph);
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

        private void doQR()
        {
            BitmapImage bitmap = new BitmapImage();
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://bom.firpo.ru/Public/86");
            gen.Parameters.Barcode.XDimension.Pixels = 34;

            string dataDir = @"C:\Users\10210795\source\repos\ToyStoreByVlasovAndry\ToyStoreByVlasovAndry\";
            gen.Save(dataDir + a.ToString() + "1.png", BarCodeImageFormat.Png);

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(dataDir + a.ToString() + "1.png");
            bitmap.EndInit();

            QRimg.Source = bitmap;
            a++;
        }
    }
}
