using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace libapp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminReadersPage.xaml
    /// </summary>
    public partial class AdminReadersPage : Window
    {
        Administrator admin = null;
        public AdminReadersPage(Administrator administrator)
        {
            InitializeComponent();
            admin = administrator;
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "server=localhost;database=libapp;username=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT surname AS 'Surname', name AS 'Name', pesel AS 'PESEL number', birthday AS 'Birthday', phone AS 'Phone number', email AS 'E-mail', address AS 'Address' FROM readers ORDER BY surname ASC", connection);


                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdDataBase);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    readers_table.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
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
            this.InvalidateVisual();
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

        private void readers_table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (readers_table.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)readers_table.SelectedItem;
                string selectedValue = selectedRow["PESEL number"].ToString();
                pesel_textbox.Text = selectedValue;
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            AdminMainPage adminmainpage = new AdminMainPage(admin);
            adminmainpage.Show();
            this.Hide();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string selectedPesel = pesel_textbox.Text;

            if (!string.IsNullOrEmpty(selectedPesel))
            {
                AdminReaderDataPage adminreaderdatapage = new AdminReaderDataPage(admin, selectedPesel);
                adminreaderdatapage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please, select a valid Pesel before proceeding.");
            }
        }
    }
}
