using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfApp8
{
    public partial class SessionPage : Page
    {
        private CinemaHall _session;
        private List<SeatModel> _allSeats;
        private List<SeatModel> _selectedSeats = new List<SeatModel>();
        private decimal _chairPrice;

        public SessionPage(CinemaHall selectedSession)
        {
            InitializeComponent();
            _session = selectedSession;

           
            LoadSessionData();
            LoadSeats();
        }

        private void LoadSessionData()
        {
            try
            {
                
                _session.Cinema = Core.Context.Cinema
                    .FirstOrDefault(c => c.ID == _session.CinemaID);

               
                _session.Hall = Core.Context.Hall
                    .FirstOrDefault(h => h.ID == _session.HallID);

                
                if (_session.Hall != null)
                {
                    _session.Hall.RatingHall = Core.Context.RatingHall
                        .FirstOrDefault(rh => rh.ID == _session.Hall.RatingHallID);
                }

                
                if (_session.Cinema != null)
                    FilmNameTextBlock.Text = _session.Cinema.Name;

                DateTextBlock.Text = _session.DateTime.ToString("dd.MM.yyyy");
                TimeTextBlock.Text = _session.DateTime.ToString("HH:mm");

                if (_session.Hall != null)
                {
                    HallNumberTextBlock.Text = _session.Hall.HallNumber;

                    if (_session.Hall.RatingHall != null)
                    {
                        RatingNameTextBlock.Text = _session.Hall.RatingHall.RatingName;
                        _chairPrice = _session.Hall.RatingHall.ChairPrice;
                        PriceTextBlock.Text = $"{_chairPrice} ₽";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void LoadSeats()
        {
            try
            {
                
                var allChairs = Core.Context.Chair
                    .Where(ch => ch.HallID == _session.Hall.ID)
                    .OrderBy(ch => ch.NumberChair)
                    .ToList();

                
                var takenChairs = Core.Context.CinemaHallChair
                    .Where(chc => chc.CinemaHallID == _session.ID && chc.IsTaken == true)
                    .Select(chc => chc.ChairID)
                    .ToList();

               
                var bookedChairs = Core.Context.Ticket
                    .Where(t => t.CinemaHallID == _session.ID)
                    .Select(t => t.ChairID)
                    .ToList();

                var unavailableChairs = takenChairs.Union(bookedChairs).ToList();

               
                _allSeats = allChairs.Select((ch, index) => new SeatModel
                {
                    ChairId = ch.ID,
                    SeatNumber = (index + 1).ToString(),
                    IsAvailable = !unavailableChairs.Contains(ch.ID),
                    IsSelected = false
                }).ToList();

               
                var row = new SeatRowModel
                {
                    RowNumber = "1",
                    Seats = _allSeats
                };

                var rows = new List<SeatRowModel> { row };
                SeatsByRow.ItemsSource = rows;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки мест: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var seat = button.Tag as SeatModel;

            if (!seat.IsAvailable)
            {
                MessageBox.Show("Это место уже занято!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            seat.IsSelected = !seat.IsSelected;

            if (seat.IsSelected)
            {
                _selectedSeats.Add(seat);
                button.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                button.Foreground = Brushes.White;
            }
            else
            {
                _selectedSeats.Remove(seat);
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            }

            UpdateSelectionInfo();
        }

        private void UpdateSelectionInfo()
        {
            SelectedCountTextBlock.Text = _selectedSeats.Count.ToString();

            decimal total = 0;
            foreach (var seat in _selectedSeats)
            {
                total += _chairPrice;
            }
            TotalPriceTextBlock.Text = $"{total} ₽";

            BookButton.IsEnabled = _selectedSeats.Count > 0;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var seat in _selectedSeats)
            {
                seat.IsSelected = false;
            }
            _selectedSeats.Clear();
            UpdateSelectionInfo();
            LoadSeats();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSeats.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одно место!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CurrentUser.ClientId == null)
            {
                MessageBoxResult result = MessageBox.Show("Для бронирования необходимо войти в систему. Перейти на страницу входа?",
                    "Требуется авторизация", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new LoginPage());
                }
                return;
            }

            try
            {
                foreach (var seat in _selectedSeats)
                {
                    var cinemaHallChair = new CinemaHallChair();
                    cinemaHallChair.CinemaHallID = _session.ID;
                    cinemaHallChair.ChairID = seat.ChairId;
                    cinemaHallChair.IsTaken = true;
                    Core.Context.CinemaHallChair.Add(cinemaHallChair);

                    var ticket = new Ticket();
                    ticket.ClientID = CurrentUser.ClientId.Value;
                    ticket.CinemaHallID = _session.ID;
                    ticket.ChairID = seat.ChairId;
                    ticket.PurchaseDate = DateTime.Now;
                    ticket.Price = _chairPrice;
                    Core.Context.Ticket.Add(ticket);
                }
                Core.Context.SaveChanges();

                MessageBox.Show($"Билеты успешно оформлены!", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                NavigationService.Navigate(new MainPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оформлении билетов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class SeatRowModel
    {
        public string RowNumber { get; set; }
        public List<SeatModel> Seats { get; set; }
    }

    public class SeatModel
    {
        public int ChairId { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsSelected { get; set; }
    }
}