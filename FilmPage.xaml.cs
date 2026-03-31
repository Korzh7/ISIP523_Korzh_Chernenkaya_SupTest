using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp8
{
    public partial class FilmPage : Page
    {
        public Cinema CurrentCinema { get; set; }
        public List<SessionModel> Sessions { get; set; }

        public FilmPage(Cinema selectedCinema)
        {
            InitializeComponent();
            CurrentCinema = selectedCinema;
            DataContext = this;

            LoadSessions();
        }

        private void LoadSessions()
        {
            try
            {
               
                Sessions = Core.Context.CinemaHall
                    .Where(ch => ch.CinemaID == CurrentCinema.ID)
                    .Select(ch => new SessionModel
                    {
                        Id = ch.ID, 
                        DateTime = ch.DateTime,
                        HallNumber = ch.Hall.HallNumber,
                        RatingName = ch.Hall.RatingHall.RatingName,
                        ChairPrice = ch.Hall.RatingHall.ChairPrice,
                        Description = ch.Hall.RatingHall.Description
                    })
                    .OrderBy(ch => ch.DateTime)
                    .ToList();

               
                DataContext = null;
                DataContext = this;

                if (Sessions != null && Sessions.Any())
                {
                    NoSessionsBorder.Visibility = Visibility.Collapsed;
                    SessionsListBox.Visibility = Visibility.Visible;
                }
                else
                {
                    SessionsListBox.Visibility = Visibility.Collapsed;
                    NoSessionsBorder.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сеансов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                NoSessionsBorder.Visibility = Visibility.Visible;
                SessionsListBox.Visibility = Visibility.Collapsed;
            }
        }

        private void SessionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SessionsListBox.SelectedItem is SessionModel selectedSession)
            {
                SessionsListBox.SelectedItem = null;

                // Проверяем авторизацию
                if (!CurrentUser.IsAuthenticated)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Для бронирования билетов необходимо войти в систему. Хотите перейти на страницу входа?",
                        "Требуется авторизация",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new LoginPage());
                    }
                    return;
                }

                // Загружаем полный CinemaHall со всеми связанными данными
                var cinemaHall = Core.Context.CinemaHall
                    .FirstOrDefault(ch => ch.ID == selectedSession.Id);

                if (cinemaHall != null)
                {
                    // Загружаем связанные данные вручную
                    cinemaHall.Cinema = Core.Context.Cinema
                        .FirstOrDefault(c => c.ID == cinemaHall.CinemaID);

                    cinemaHall.Hall = Core.Context.Hall
                        .FirstOrDefault(h => h.ID == cinemaHall.HallID);

                    if (cinemaHall.Hall != null)
                    {
                        cinemaHall.Hall.RatingHall = Core.Context.RatingHall
                            .FirstOrDefault(rh => rh.ID == cinemaHall.Hall.RatingHallID);
                    }

                    // Передаем полностью загруженный объект
                    NavigationService.Navigate(new SessionPage(cinemaHall));
                }
            }
        }
    }
}