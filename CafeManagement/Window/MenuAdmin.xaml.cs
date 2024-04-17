using CafeManagement.Classes;
using CafeManagement.Model;
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
    /// Логика взаимодействия для MenuAdmin.xaml
    /// </summary>
    public partial class MenuAdmin : System.Windows.Window
    {
        public MenuAdmin()
        {
            InitializeComponent();
            if (AllData.ID != 0)
            {
                User user = App.context.Users.ToList().Find(u => u.UserId == AllData.ID);
                name.Content = user.FIO;
                dolj.Content = user.dolgnost;
            }
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            UsersAdmin usersAdmin = new UsersAdmin();
            usersAdmin.Show();
            this.Close();
        }

        private void Shifts_Click(object sender, RoutedEventArgs e)
        {
            ShiftsAdmin shiftsAdmin = new ShiftsAdmin();
            shiftsAdmin.Show();
            this.Close();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            OrdersMenu ordersMenu = new OrdersMenu();
            ordersMenu.Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
