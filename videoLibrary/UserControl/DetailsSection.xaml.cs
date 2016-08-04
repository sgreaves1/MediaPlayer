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
                new PropertyMetadata(null));
        
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
            set { SetValue(SelectedItemProperty, value); }
        }
    }
}
