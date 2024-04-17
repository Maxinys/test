using CafeManagement.Classes;
using CafeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CafeManagement.Window
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : System.Windows.Window
    {
        private int dishCounter = 1;
        private int? orderNumber;
        public AddOrder()
        {
            InitializeComponent();

            List<Dish> participantsList = App.context.Dishes.ToList();
            Diches.ItemsSource = participantsList;
            orderNumber = null;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OrdersWaiter ordersWaiter = new OrdersWaiter();
            ordersWaiter.Show();
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заказ успешно создан!", "Уведомление", MessageBoxButton.OK);
            OrdersWaiter ordersWaiter = new OrdersWaiter();
            ordersWaiter.Show();
            this.Close();
        }

        private void AddDish_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Diches.Text) || string.IsNullOrWhiteSpace(Quantity.Text) || string.IsNullOrWhiteSpace(Number.Text) || string.IsNullOrWhiteSpace(QuantityClient.Text) || Convert.ToInt32(Number.Text) == 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля перед сохранением.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (orderNumber == null)
            {
                Order newOrder = new()
                {
                    OrderStatus = "Новый",
                    ShiftId = 3,
                    TableNumber = int.Parse(QuantityClient.Text),
                    CustomerCount = int.Parse(QuantityClient.Text)
                };

                App.context.Orders.Add(newOrder);
                App.context.SaveChanges();
                orderNumber = newOrder.OrderId;
                QuantityClient.IsEnabled = false;
                Number.IsEnabled = false;
            }
            Dish selectedDish = Diches.SelectedItem as Dish;

            if (selectedDish != null)
            {
                if (App.context.Orderdishes.Any(od => od.OrderId == orderNumber && od.DishId == selectedDish.DishId))
                {
                    MessageBox.Show("Это блюдо уже добавлено в заказ.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Orderdish orderDish = new()
                {
                    OrderId = orderNumber,
                    DishId = selectedDish.DishId,
                    Quantity = int.Parse(Quantity.Text)
                };

                App.context.Orderdishes.Add(orderDish);
                App.context.SaveChanges();

                ordersListView.Items.Add(new
                {
                    Номер = dishCounter,
                    Блюдо = selectedDish.DishName,
                    Количество = orderDish.Quantity,
                    Цена = selectedDish.Price
                });

                dishCounter++;
                Diches.SelectedIndex = -1;
                Quantity.Clear();
            }
        }
    }
}
