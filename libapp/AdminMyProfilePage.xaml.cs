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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace libapp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminMyProfilePage.xaml
    /// </summary>
    public partial class AdminMyProfilePage : Window
    {
        Administrator admin = null;
        public AdminMyProfilePage(Administrator administrator)
        {
            InitializeComponent();

            name_label.Content = administrator.name;
            surname_label.Content = administrator.surname;
            login_textbox.Content = administrator.login;
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
            this.InvalidateVisual();
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
            AdminEditProfilePage admineditprofilepage = new AdminEditProfilePage(admin);
            admineditprofilepage.Show();
            this.Hide();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            int adminId = admin.id;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete your account?", "libapp", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    string connectionString = "server=localhost;database=libapp;username=root;password=;";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = "DELETE FROM administrators WHERE id = @id";
                        MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@id", adminId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Account successfully removed.", "libapp");
                        }
                        else
                        {
                            MessageBox.Show("Account not found.", "libapp");
                        }

                        connection.Close();

                        AdminLoginPage adminLoginPage = new AdminLoginPage();
                        adminLoginPage.Show();
                        this.Hide();
                    }
                }
                catch
                {
                    MessageBox.Show("Error removing profile.", "libapp");
                }
            }
            else
            {
                this.InvalidateVisual();
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            AdminMainPage adminmainpage = new AdminMainPage(admin);
            adminmainpage.Show();
            this.Hide();
        }
    }
}
