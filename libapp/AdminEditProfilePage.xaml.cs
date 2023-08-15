using MySql.Data.MySqlClient;
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
using System.Xml.Linq;

namespace libapp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminEditProfilePage.xaml
    /// </summary>
    public partial class AdminEditProfilePage : Window
    {
        Administrator admin = null;
        public AdminEditProfilePage(Administrator administrator)
        {
            InitializeComponent();

            name_textbox.Text = administrator.name;
            surname_textbox.Text = administrator.surname;
            login_textbox.Text = administrator.login;
            pesel_label.Content = administrator.pesel;

            admin = administrator;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AdminMainPage adminmainpage = new AdminMainPage(admin);
            adminmainpage.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminMyProfilePage adminmyprofilepage = new AdminMyProfilePage(admin);
            adminmyprofilepage.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminReadersPage adminreaderspage = new AdminReadersPage(admin);
            adminreaderspage.Show();
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AdminAddReaderPage adminaddreaderpage = new AdminAddReaderPage(admin);
            adminaddreaderpage.Show();
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AdminBorrowBookPage adminborrowbookpage = new AdminBorrowBookPage(admin);
            adminborrowbookpage.Show();
            this.Hide();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            AdminBookListPage adminbooklistpage = new AdminBookListPage(admin);
            adminbooklistpage.Show();
            this.Hide();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            AdminReturnDeadlinesPage adminreturndeadlinespage = new AdminReturnDeadlinesPage(admin);
            adminreturndeadlinespage.Show();
            this.Hide();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            AdminAddBookPage adminaddbookpage = new AdminAddBookPage(admin);
            adminaddbookpage.Show();
            this.Hide();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AdminLoginPage adminloginpage = new AdminLoginPage();
            adminloginpage.Show();
            this.Hide();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string name = name_textbox.Text;
            string surname = surname_textbox.Text;
            string login = login_textbox.Text;
            string pesel = pesel_label.Content.ToString();
            string password = password_textbox.Text;
            string confirmPassword = confirmpassword_textbox.Text;

            if (name.Length == 0 || surname.Length == 0 || login.Length == 0 || password.Length == 0 || confirmPassword.Length == 0)
            {
                MessageBox.Show("Complete the empty fields.", "libapp");
                return;
            }
            if (login.Length < 5 || login.Length > 15)
            {
                MessageBox.Show("The login must contain from 5 to 15 characters.", "libapp");
                return;
            }
            if ((password.Length < 5 && password.Length != 0) || (password.Length > 20 && password.Length != 0))
            {
                MessageBox.Show("The password must contain between 5 and 20 characters.", "libapp");
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("The passwords provided are different. Correct the data.", "libapp");
                return;
            }
            try
            {
                string connectionString = "server=localhost;database=libapp;username=root;password=;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE administrators SET name='" + name + "', surname='" + surname + "', login='" + login + "', password='" + password + "';";
                    MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                    command.ExecuteNonQuery();
                    admin.name = this.name_textbox.Text;
                    admin.surname = this.surname_textbox.Text;
                    admin.login = this.login_textbox.Text;
                    admin.password = this.password_textbox.Text;

                    MessageBox.Show("Successfully corrected the data.", "libapp");
                    connection.Close();

                    AdminMyProfilePage adminmyprofilepage = new AdminMyProfilePage(admin);
                    adminmyprofilepage.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Account edit error. Correct the data.", "libapp");
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            AdminMyProfilePage adminmyprofilepage = new AdminMyProfilePage(admin);
            adminmyprofilepage.Show();
            this.Hide();
        }
    }
}
