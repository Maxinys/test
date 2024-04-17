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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : System.Windows.Window
    {
        public Authorization()
        {
            InitializeComponent();
            Password.PasswordChanged += Password_PasswordChanged;
            Password.Visibility = Visibility.Visible;
            Password.Password = Password2.Text;
        }
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password2.Text = Password.Password;
        }
        private void GuestEnter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Password2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Password.Password = Password2.Text;
        }
        private void Safety_Checked(object sender, RoutedEventArgs e)
        {
            Password2.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Collapsed;
            Password2.Text = Password.Password;
        }

        private void Safety_Unchecked(object sender, RoutedEventArgs e)
        {
            Password2.Visibility = Visibility.Collapsed;
            Password.Visibility = Visibility.Visible;
            Password.Password = Password2.Text;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length != 0 && Password.Password.Length != 0 && Password2.Text.Length != 0)
            {
                User user = App.context.Users.ToList().Find(u => u.Username == Login.Text && u.Password == Password.Password && u.Password == Password2.Text);

                if (user != null)
                {

                    if (user.Role == "Администратор")
                    {
                        AllData.ID = user.UserId;
                        MessageBox.Show($"Добро пожаловать, {user.dolgnost} {user.FIO} !");
                        MenuAdmin adminMenu = new MenuAdmin();
                        adminMenu.Show();
                        this.Close();
                        return;
                    }
                    else if (user.Role == "Повар")
                    {
                        AllData.ID = user.UserId;
                        MessageBox.Show($"Добро пожаловать, {user.dolgnost} {user.FIO} !");
                        MenuCook cookMenu = new MenuCook();
                        cookMenu.Show();
                        this.Close();
                        return;
                    }
                    else if (user.Role == "Официант")
                    {
                        AllData.ID = user.UserId;
                        MessageBox.Show($"Добро пожаловать, {user.dolgnost} {user.FIO} !");
                        MenuWaiter waiterMenu = new MenuWaiter();
                        waiterMenu.Show();
                        this.Close();
                        return;
                    }
                }

                else
                {
                    MessageBox.Show("Вы ввели неверные данные!");
                }
            }
            else
            {
                MessageBox.Show("Вы должны заполнить все поля!");
            }
        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

