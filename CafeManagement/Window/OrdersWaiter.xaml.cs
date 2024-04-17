using CafeManagement.Classes;
using CafeManagement.Model;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace CafeManagement.Window
{
    /// <summary>
    /// Логика взаимодействия для OrdersWaiter.xaml
    /// </summary>
    public partial class OrdersWaiter : System.Windows.Window
    {
        public OrdersWaiter()
        {
            InitializeComponent();
            if (AllData.ID != 0)
            {
                User user = App.context.Users.ToList().Find(u => u.UserId == AllData.ID);
                name.Content = user.FIO;
                dolj.Content = user.dolgnost;
            }
            LoadOrders();
        }
        private void LoadOrders()
        {
            using (var dbContext = new CafeManagementContext())
            {
                var orders = dbContext.Orders
                    .Include(o => o.Orderdishes)
                        .ThenInclude(od => od.Dish)
                    .ToList();

                var orderData = orders.Select(order => new
                {
                    order.OrderId,
                    order.TableNumber,
                    order.CustomerCount,
                    order.OrderStatus,
                    DishList = order.Orderdishes.Select(od => new
                    {
                        DishName = od.Dish?.DishName
                    }).ToList()
                }).ToList();

                ordersListView.ItemsSource = orderData;
            }
        }
        private void MarkOrderAsReady_Click(object sender, RoutedEventArgs e)
        {
            if (ordersListView.SelectedItem != null)
            {
                var selectedOrder = (dynamic)ordersListView.SelectedItem;
                int orderId = selectedOrder.OrderId; // Используем OrderId вместо TableNumber
                UpdateOrderStatus(orderId, "Оплачен");
                LoadOrders();
            }
        }

        private void UpdateOrderStatus(int tableNumber, string newStatus)
        {
            using (var dbContext = new CafeManagementContext())
            {
                var orderToUpdate = dbContext.Orders.FirstOrDefault(o => o.OrderId == tableNumber);

                if (orderToUpdate != null)
                {
                    orderToUpdate.OrderStatus = newStatus;
                    dbContext.SaveChanges();
                }
            }
        }

        private void ordersListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ordersListView.SelectedItem != null)
            {
                var selectedOrder = (dynamic)ordersListView.SelectedItem;
                int orderId = selectedOrder.OrderId; // Используем OrderId вместо TableNumber

                MessageBox.Show($"Выбран заказ с номером {orderId}!");
            }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MenuCook menuCook = new MenuCook();
            menuCook.Show();
            this.Close();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            int newOrderNumber = GetNewOrderNumber();

            AddOrder addorder = new AddOrder();
            addorder.Show();
            this.Close();
        }

        private int GetNewOrderNumber()
        {
            return 1;
        }
    }
}
