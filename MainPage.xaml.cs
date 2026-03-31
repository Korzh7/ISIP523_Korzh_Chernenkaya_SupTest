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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private List<Cinema> _allCinemas;
        private string _searchText = "";
        private string _currentSort = "";

        public MainPage()
        {
            InitializeComponent();
            LoadCinemas();
        }

        private void LoadCinemas()
        {
            try
            {
               
                _allCinemas = Core.Context.Cinema.ToList();
                    
                CinemaListBox.ItemsSource = _allCinemas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фильмов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyFilter()
        {
            try
            {
                if (_allCinemas == null) return;

                IEnumerable<Cinema> query = _allCinemas;

               
                if (!string.IsNullOrWhiteSpace(_searchText))
                {
                    query = query.Where(c => c.Name != null &&
                        c.Name.IndexOf(_searchText, StringComparison.OrdinalIgnoreCase) >= 0);
                }

               
                if (_currentSort == "Name")
                {
                    query = query.OrderBy(c => c.Name);
                }
                else if (_currentSort == "Rating")
                {
                    query = query.OrderByDescending(c => c.Rating);
                }

                CinemaListBox.ItemsSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchText = SearchTextBox.Text;
            ApplyFilter();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            NameSortRadio.IsChecked = false;
            RatingSortRadio.IsChecked = false;
            _currentSort = "";
            ApplyFilter();
        }

        private void NameSort_Checked(object sender, RoutedEventArgs e)
        {
            _currentSort = "Name";
            ApplyFilter();
        }

        private void RatingSort_Checked(object sender, RoutedEventArgs e)
        {
            _currentSort = "Rating";
            ApplyFilter();
        }

        private void CinemaListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CinemaListBox.SelectedItem is Cinema selectedCinema)
            {
                CinemaListBox.SelectedItem = null;
                NavigationService.Navigate(new FilmPage(selectedCinema));
            }
        }
    }
}