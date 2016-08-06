using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using LibrarySamples.Command;
using videoLibrary.Model;
using System.Net;

namespace videoLibrary.UserControl
{
    /// <summary>
    /// Interaction logic for DetailsSection.xaml
    /// </summary>
    public partial class DetailsSection 
    {
        /// <summary>
        /// Dependency property to back the <see cref="SelectedItem"/> property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem",
                typeof(Film),
                typeof(DetailsSection),
                new PropertyMetadata(null, GetDetails));

        /// <summary>
        /// Dependency property to back the <see cref="SelectedSeason"/> property.
        /// </summary>
        public static readonly DependencyProperty SelectedSeasonProperty =
            DependencyProperty.Register("SelectedSeason",
                typeof(Season),
                typeof(DetailsSection),
                new PropertyMetadata(null, GetSeasonDetails));
        
        /// <summary>
        /// Constructor
        /// </summary>
        public DetailsSection()
        {
            InitializeComponent();

            InitCommands();
        }

        /// <summary>
        /// The selected item of this details section.
        /// </summary>
        public Film SelectedItem
        {
            get { return (Film)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        /// <summary>
        /// The selected season of this details section.
        /// </summary>
        public Season SelectedSeason
        {
            get { return (Season)GetValue(SelectedSeasonProperty); }
            set { SetValue(SelectedSeasonProperty, value); }
        }
        
        private static void GetDetails(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DetailsSection uc = (DetailsSection)dependencyObject;

            if (uc?.SelectedItem != null)
            {
                using (WebClient wc = new WebClient())
                {
                    string url = "http://www.omdbapi.com/?t=" + uc.SelectedItem.Name + "&y=&plot=full&r=json";
                    var jsonString = wc.DownloadString(url);

                    if (jsonString.Contains("Released"))
                    {
                        var start = jsonString.IndexOf("Released") + 11;
                        var match = jsonString.Substring(start);

                        var released = match.Substring(0, match.IndexOf("\""));

                        uc.SelectedItem.ReleaseDate = released;
                    }

                    if (jsonString.Contains("Plot"))
                    {
                        var start = jsonString.IndexOf("Plot") + 7;
                        var match = jsonString.Substring(start);

                        var plot = match.Substring(0, match.IndexOf("\""));

                        uc.SelectedItem.Synopsis = plot;
                        
                    }                    
                }

                if (uc.SelectedItem.Seasons != null)
                {
                    foreach (Season season in uc.SelectedItem.Seasons)
                    {
                        season.IsSelected = false;
                    }
                }
            }
        }

        private static void GetSeasonDetails(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            
        }

        #region Commands

        public ICommand SeasonSelectedCommand { get; set; }

        private void InitCommands()
        {
            SeasonSelectedCommand = new DelegateCommand(ExecuteSeasonSelectedCommand, CanExecuteSeasonSelectedCommand);
        }

        private bool CanExecuteSeasonSelectedCommand(object o)
        {
            return true;
        }

        private void ExecuteSeasonSelectedCommand(object sender)
        {
            SelectedSeason = (Season)((ToggleButton)sender).DataContext;

            foreach (var season in SelectedItem.Seasons)
            {
                season.IsSelected = false;
            }

            SelectedSeason.IsSelected = true;

        }

        #endregion
    }
}
