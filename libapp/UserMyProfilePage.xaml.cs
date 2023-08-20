using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
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
    /// Logika interakcji dla klasy UserMyProfilePage.xaml
    /// </summary>
    public partial class UserMyProfilePage : Window
    {
        Reader us = null;
        public UserMyProfilePage(Reader user)
        {
            InitializeComponent();
            us = user;

            name_label.Content = us.name;
            surname_label.Content = us.surname;
            pesel_label.Content = us.pesel;
            phone_label.Content = us.phone;
            email_label.Content = us.email;
            birthday_label.Content = us.birthday;
            address_label.Content = us.address;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UserMainPage usermainpage = new UserMainPage(us);
            usermainpage.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.InvalidateVisual();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            UserReturnDeadlinesPage userreturndeadlinespage = new UserReturnDeadlinesPage(us);
            userreturndeadlinespage.Show();
            this.Hide();
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

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            UserEditProfilePage usereditprofilepage = new UserEditProfilePage(us);
            usereditprofilepage.Show();
            this.Hide();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            UserMainPage usermainpage = new UserMainPage(us);
            usermainpage.Show();
            this.Hide();
        }
    }
}
