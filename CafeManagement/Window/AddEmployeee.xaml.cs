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
    /// Логика взаимодействия для AddEmployeee.xaml
    /// </summary>
    public partial class AddEmployeee : System.Windows.Window
    {
        public AddEmployeee()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти? Новые данные не сохранятся.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                UsersAdmin usersAdmin = new UsersAdmin();
                usersAdmin.Show();
                this.Close();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                Role = Role.Text,
                Username = Username.Text,
                Password = Password.Text,
            };
            CafeManagementContext context = CafeManagementContext.GetContext();
            context.Users.Add(newUser);
            context.SaveChanges();
            Employee newEmployee = new Employee
            {
                FirstName = Surname.Text,
                LastName = Name.Text,
                Status = "Работает",
                UserId = newUser.UserId,
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();

            MessageBox.Show("Успешно добавлено");
            UsersAdmin usersAdmin = new UsersAdmin();
            usersAdmin.Show();
            this.Close();

        }

    }
}
