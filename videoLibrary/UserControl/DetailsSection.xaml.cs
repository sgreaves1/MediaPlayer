using System;
using System.Windows;
using videoLibrary.Model;

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
            set
            {
                SetValue(SelectedSeasonProperty, value);
            }
        }

        private static void GetDetails(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DetailsSection uc = (DetailsSection)dependencyObject;

            if (uc.SelectedItem != null)
            {
                if (System.IO.File.Exists(uc.SelectedItem.Details))
                {
                    string[] lines = System.IO.File.ReadAllLines(uc.SelectedItem.Details);

                    uc.SelectedItem.Year = int.Parse(lines[1]);
                    uc.SelectedItem.Synopsis = lines[2];
                }
                else
                {
                    uc.SelectedItem.Synopsis = "N/A";
                }


                foreach (Season season in uc.SelectedItem.Seasons)
                {
                    season.IsSelected = false;
                }
            }
        }

        private static void GetSeasonDetails(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
