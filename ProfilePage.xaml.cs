using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp8
{
    public partial class ProfilePage : Page
    {
        public Client Client { get; set; }
        public List<Ticket> Tickets { get; set; }

        public ProfilePage()
        {
            InitializeComponent();

            if (!CurrentUser.IsAuthenticated)
            {
                MessageBox.Show("Необходимо войти в систему", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                NavigationService.GoBack();
                return;
            }

            LoadUserData();
            LoadTickets();
            DataContext = this;
        }

        private void LoadUserData()
        {
            try
            {
                Client = Core.Context.Client
                    .FirstOrDefault(c => c.ID == CurrentUser.ClientId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных пользователя: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTickets()
        {
            try
            {
                Tickets = Core.Context.Ticket
                    .Where(t => t.ClientID == CurrentUser.ClientId.Value)
                    .ToList();

                
                foreach (var ticket in Tickets)
                {
                    ticket.CinemaHall = Core.Context.CinemaHall
                        .Include("Cinema")
                        .Include("Hall")
                        .Include("Hall.RatingHall")
                        .FirstOrDefault(ch => ch.ID == ticket.CinemaHallID);

                    ticket.Chair = Core.Context.Chair
                        .FirstOrDefault(ch => ch.ID == ticket.ChairID);
                }

                
                Tickets = Tickets.OrderByDescending(t => t.PurchaseDate).ToList();

                if (Tickets != null && Tickets.Any())
                {
                    TicketsListBox.ItemsSource = Tickets;
                    TicketsListBox.Visibility = Visibility.Visible;
                    NoTicketsBorder.Visibility = Visibility.Collapsed;
                }
                else
                {
                    TicketsListBox.Visibility = Visibility.Collapsed;
                    NoTicketsBorder.Visibility = Visibility.Visible;
                }

                
                DataContext = null;
                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки билетов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                NoTicketsBorder.Visibility = Visibility.Visible;
                TicketsListBox.Visibility = Visibility.Collapsed;
            }
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tickets == null) return;

            switch (SortComboBox.SelectedIndex)
            {
                case 0: 
                    Tickets = Tickets.OrderByDescending(t => t.PurchaseDate).ToList();
                    break;
                case 1: 
                    Tickets = Tickets.OrderBy(t => t.PurchaseDate).ToList();
                    break;
                case 2: 
                    Tickets = Tickets.OrderBy(t => t.CinemaHall.Cinema.Name).ToList();
                    break;
                case 3:
                    Tickets = Tickets.OrderBy(t => t.Price).ToList();
                    break;
                case 4:
                    Tickets = Tickets.OrderByDescending(t => t.Price).ToList();
                    break;
            }

            TicketsListBox.ItemsSource = null;
            TicketsListBox.ItemsSource = Tickets;
        }

        private void BuyTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new MainPage());
                mainWindow.PageTitleTextBlock.Text = "Главная страница";
                mainWindow.BackButton.Visibility = Visibility.Collapsed;
            }
        }
    }

   
   
}