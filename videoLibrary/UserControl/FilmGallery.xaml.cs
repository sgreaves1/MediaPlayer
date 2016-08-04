using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using videoLibrary.Model;

namespace videoLibrary.UserControl
{
    /// <summary>
    /// Interaction logic for FilmGallery.xaml
    /// </summary>
    public partial class FilmGallery
    {
        /// <summary>
        /// Dependency Property to back the <see cref="ItemsSource"/> property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", 
                typeof(ObservableCollection<Film>), 
                typeof(FilmGallery), 
                new PropertyMetadata(null));

        /// <summary>
        ///  Dependency Property to back the <see cref="SelectedItem"/> property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", 
                typeof(Film), 
                typeof(FilmGallery),
                new PropertyMetadata(null));
        
        /// <summary>
        ///  Dependency Property to back the <see cref="ItemSelectedCommand"/> property.
        /// </summary>
        public static readonly DependencyProperty ItemSelectedCommandProperty =
            DependencyProperty.Register("ItemSelectedCommand", 
                typeof(ICommand), 
                typeof(FilmGallery), 
                new PropertyMetadata(null));



        /// <summary>
        /// Constructor
        /// </summary>
        public FilmGallery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A collection of items to display in this gallery.
        /// </summary>
        public ObservableCollection<Film> ItemsSource
        {
            get { return (ObservableCollection<Film>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// The selected item of this gallery.
        /// </summary>
        public Film SelectedItem
        {
            get { return (Film)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Command to run when an item is selected
        /// </summary>
        public ICommand ItemSelectedCommand
        {
            get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
            set { SetValue(ItemSelectedCommandProperty, value); }
        }
    }
}
