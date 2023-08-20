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
            string adminName = name_textbox.Text;
            string adminSurname = surname_textbox.Text;
            string adminLogin = login_textbox.Text;
            string adminPesel = pesel_label.Content.ToString();
            string adminPassword = password_box.Password;
            string adminConfirmPassword = confirmPassword_box.Password;

            if (string.IsNullOrEmpty(adminName) || string.IsNullOrEmpty(adminSurname) || string.IsNullOrEmpty(adminLogin) || string.IsNullOrEmpty(adminPassword) || string.IsNullOrEmpty(adminConfirmPassword))
            {
                MessageBox.Show("Complete the empty fields.", "libapp");
                return;
            }

            if (adminLogin.Length < 5 || adminLogin.Length > 15)
            {
                MessageBox.Show("The login must contain from 5 to 15 characters.", "libapp");
                return;
            }

            if ((adminPassword.Length < 5 && adminPassword.Length != 0) || (adminPassword.Length > 20 && adminPassword.Length != 0))
            {
                MessageBox.Show("The password must contain between 5 and 20 characters.", "libapp");
                return;
            }

            if (adminPassword != adminConfirmPassword)
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

                    string updateQuery = "UPDATE administrators SET name=@name, surname=@surname, login=@login, password=@password WHERE pesel=@pesel";

                    MySqlCommand command = new MySqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@name", adminName);
                    command.Parameters.AddWithValue("@surname", adminSurname);
                    command.Parameters.AddWithValue("@login", adminLogin);
                    command.Parameters.AddWithValue("@password", adminPassword);
                    command.Parameters.AddWithValue("@pesel", adminPesel);

                    command.ExecuteNonQuery();

                    admin.name = adminName;
                    admin.surname = adminSurname;
                    admin.login = adminLogin;
                    admin.password = adminPassword;

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
