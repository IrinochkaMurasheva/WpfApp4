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
using Microsoft.EntityFrameworkCore;
using WpfApp4;

namespace SQLiteApp
{
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        // при загрузке окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Users.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Users.Local.ToObservableCollection();
           
        }

        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow ProductWindow = new ProductWindow(new Product());
            if (ProductWindow.ShowDialog() == true)
            {
                Product Product = ProductWindow.Product;
                db.Users.Add(Product);
                db.SaveChanges();
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Product? product = productsList.SelectedItem as Product;
            // если ни одного объекта не выделено, выходим
            if (product is null) return;

            ProductWindow ProductWindow = new ProductWindow(new Product
            {
                Id = product.Id,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description
               
            });

            if (ProductWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                product = db.Users.Find(ProductWindow.Product.Id);
                if (product != null)
                {
                    product.Price = ProductWindow.Product.Price;
                    product.Name = ProductWindow.Product.Name;
                    product.Description = ProductWindow.Product.Description;
                    db.SaveChanges();
                    productsList.Items.Refresh();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Product? product = productsList.SelectedItem as Product;
            // если ни одного объекта не выделено, выходим
            if (product is null) return;
            db.Users.Remove(product);
            db.SaveChanges();
        }
        
        
    }
  
}