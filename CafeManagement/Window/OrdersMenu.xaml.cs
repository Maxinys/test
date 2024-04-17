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
    /// Логика взаимодействия для OrdersMenu.xaml
    /// </summary>
    public partial class OrdersMenu : System.Windows.Window
    {
        public OrdersMenu()
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




        private void back_Click(object sender, RoutedEventArgs e)
        {
            MenuAdmin menuAdmin = new MenuAdmin();
            menuAdmin.Show();
            this.Close();
        }
    }
}
