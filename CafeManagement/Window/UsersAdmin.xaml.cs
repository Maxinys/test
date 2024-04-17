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
    /// Логика взаимодействия для UsersAdmin.xaml
    /// </summary>
    public partial class UsersAdmin : System.Windows.Window
    {
        private CafeManagementContext dbContext = new CafeManagementContext();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public UsersAdmin()
        {
            InitializeComponent();
            if (AllData.ID != 0)
            {
                User user = App.context.Users.ToList().Find(u => u.UserId == AllData.ID);
                name.Content = user.FIO;
                dolj.Content = user.dolgnost;
            }
            Loadata();
        }
        public void Loadata()
        {
            List<Employee> tabularsections = dbContext.Employees.Include(t => t.User).ToList();
            usersView.ItemsSource = tabularsections;
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MenuAdmin menuAdmin = new MenuAdmin();
            menuAdmin.Show();
            this.Close();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeee addEmployeee = new AddEmployeee();
            addEmployeee.Show();
            this.Close();
        }

        private void usersView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (usersView.SelectedItem != null)
            {
                Employee selectedEmployee = (Employee)usersView.SelectedItem;

                // Отображение диалогового окна с вопросом
                MessageBoxResult result = MessageBox.Show($"Вы действительно хотите сменить статус сотрудника {selectedEmployee.FirstName} {selectedEmployee.LastName} на 'Уволен'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Если пользователь выбрал 'Да', меняем статус
                    selectedEmployee.Status = "Уволен";
                    dbContext.SaveChanges();
                    Loadata();

                    MessageBox.Show($"Статус сотрудника {selectedEmployee.FirstName} {selectedEmployee.LastName} изменен на 'Уволен'.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
