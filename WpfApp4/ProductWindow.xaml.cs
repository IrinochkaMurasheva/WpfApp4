using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
using WpfApp4;

namespace SQLiteApp
{
    public partial class  ProductWindow : Window
    {
        public Product Product { get; set; }
        public ProductWindow(Product product)
        {
            InitializeComponent();
            Product = product;
            DataContext = Product;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }    
        private void qr_Click (object sender, RoutedEventArgs e) 
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qRCodeData = qrGenerator.CreateQrCode(Name.Text + ". Цена: " + Price.Text + " Описание: " + Description.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode qR = new QRCode(qRCodeData);
            Bitmap qr = qR.GetGraphic(150);
            image_qrcode.Source = Convert(qr);



        }
        public BitmapImage Convert(Bitmap scr)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)scr).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }

    
}
