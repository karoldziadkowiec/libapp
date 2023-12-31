﻿using System;
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

namespace libapp
{
    /// <summary>
    /// Logika interakcji dla klasy UserMainPage.xaml
    /// </summary>
    public partial class UserMainPage : Window
    {
        Reader us = null;
        public UserMainPage(Reader user)
        {
            InitializeComponent();
            us = user;

            // Pobierz dzisiejszą datę i godzinę
            DateTime now = DateTime.Now;

            // Przygotuj tekst z nazwą dnia tygodnia i datą
            string formattedDate = $"{now.DayOfWeek}, {now.ToString("dd/MM/yyyy")}";

            // Przypisz sformatowaną datę i godzinę do Label
            date_label.Content = formattedDate;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.InvalidateVisual();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserMyProfilePage usermyprofilepage = new UserMyProfilePage(us);
            usermyprofilepage.Show();
            this.Hide();
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
    }
}
