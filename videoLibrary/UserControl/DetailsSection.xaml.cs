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
                new PropertyMetadata(null, SelectedItemChanged));

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
        
        private static void SelectedItemChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DetailsSection uc = (DetailsSection)dependencyObject;

            if (uc?.SelectedItem != null)
            {
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
