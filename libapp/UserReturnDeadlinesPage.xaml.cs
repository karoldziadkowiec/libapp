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
    /// Logika interakcji dla klasy UserReturnDeadlinesPage.xaml
    /// </summary>
    public partial class UserReturnDeadlinesPage : Window
    {
        Reader us = null;
        public UserReturnDeadlinesPage(Reader user)
        {
            InitializeComponent();
            us = user;
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "server=localhost;database=libapp;username=root;password=;";

            string pesel = us.pesel;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmdBorrowing = new MySqlCommand("SELECT books.title AS 'Book', books.author AS 'Author', borrowing.borrow_date AS 'Borrow date', borrowing.return_date AS 'Return date', DATEDIFF(borrowing.return_date, CURDATE()) AS 'Days to return' FROM borrowing, books, readers WHERE readers.pesel = '" + pesel + "' AND borrowing.book = books.id AND borrowing.reader = readers.id ORDER BY borrowing.return_date ASC", connection);
                    MySqlDataAdapter BorrowingAdapter = new MySqlDataAdapter(cmdBorrowing);
                    DataTable borrowingDt = new DataTable();
                    BorrowingAdapter.Fill(borrowingDt);
                    borrowing_table.ItemsSource = borrowingDt.DefaultView;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UserMainPage usermainpage = new UserMainPage(us);
            usermainpage.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserMyProfilePage usermyprofilepage = new UserMyProfilePage(us);
            usermyprofilepage.Show();
            this.Hide();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.InvalidateVisual();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            UserBookListPage userbooklistpage = new UserBookListPage(us);
            userbooklistpage.Show();
            this.Hide();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            UserLoginPage userloginpage = new UserLoginPage();
            userloginpage.Show();
            this.Hide();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            UserMainPage usermainpage = new UserMainPage(us);
            usermainpage.Show();
            this.Hide();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
