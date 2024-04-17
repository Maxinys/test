using CafeManagement.Classes;
using CafeManagement.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ShiftsAdmin.xaml
    /// </summary>
    public partial class ShiftsAdmin : System.Windows.Window
    {
        // Используйте List вместо ObservableCollection для простоты примера
        public List<Shift> Shifts { get; set; } = new List<Shift>();
        public List<Employee> Employees { get; set; } = new List<Employee>();

        // Выбранные элементы
        private Shift selectedShift;
        private Employee selectedEmployee;

        public ShiftsAdmin()
        {
            InitializeComponent();
            if (AllData.ID != 0)
            {
                User user = App.context.Users.ToList().Find(u => u.UserId == AllData.ID);
                name.Content = user.FIO;
                dolj.Content = user.dolgnost;
            }
            shiftsDataGrid.ItemsSource = Shifts;
            employeesDataGrid.ItemsSource = Employees;

            LoadShifts();
            LoadEmployees();
        }

        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedShift == null)
            {
                MessageBox.Show("Выберите смену для назначения.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedEmployee == null)
            {
                MessageBox.Show("Выберите сотрудника для назначения.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка, что сотрудник не назначен на выбранную смену
            if (IsEmployeeAssigned(selectedShift.ShiftId, selectedEmployee.EmployeeId))
            {
                MessageBox.Show($"Сотрудник {selectedEmployee.FirstName} {selectedEmployee.LastName} уже назначен на выбранную смену.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Shiftemployee newAssignment = new Shiftemployee
            {
                ShiftId = selectedShift.ShiftId,
                EmployeeId = selectedEmployee.EmployeeId
            };

            SaveAssignment(newAssignment);

            LoadShifts();
            LoadEmployees();

            MessageBox.Show($"Сотрудник {selectedEmployee.FirstName} {selectedEmployee.LastName} успешно назначен на смену '{selectedShift.ShiftType}' с {selectedShift.StartTime} до {selectedShift.EndTime}.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsEmployeeAssigned(int shiftId, int employeeId)
        {
            using (var dbContext = new CafeManagementContext())
            {
                return dbContext.Shiftemployees.Any(se => se.ShiftId == shiftId && se.EmployeeId == employeeId);
            }
        }


        private void LoadShifts()
        {
            using (var dbContext = new CafeManagementContext())
            {
                Shifts.Clear();
                var shifts = dbContext.Shifts.ToList();
                foreach (var shift in shifts)
                {
                    Shifts.Add(shift);
                }
            }
        }


        private void LoadEmployees()
        {
            using (var dbContext = new CafeManagementContext())
            {
                var workingEmployees = dbContext.Employees.Where(employee => employee.Status == "Работает").ToList();
                Employees.Clear();

                foreach (var employee in workingEmployees)
                {
                    Employees.Add(employee);
                }
            }
        }


        private void shiftsDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedShift = shiftsDataGrid.SelectedItem as Shift;
            Shifts1.Text = selectedShift != null ? $"{selectedShift.ShiftType} {selectedShift.StartTime} {selectedShift.EndTime}" : "";
        }

        private void employeesDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedEmployee = employeesDataGrid.SelectedItem as Employee;

            User user = App.context.Users.ToList().Find(u => u.UserId == selectedEmployee.UserId);
            string dolj = user.dolgnost;

            Emp.Text = selectedEmployee != null ? $"{selectedEmployee.FirstName} {selectedEmployee.LastName} {dolj}" : "";
        }

        private void SaveAssignment(Shiftemployee assignment)
        {
            using (var dbContext = new CafeManagementContext())
            {
                dbContext.Shiftemployees.Add(assignment);
                dbContext.SaveChanges();
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