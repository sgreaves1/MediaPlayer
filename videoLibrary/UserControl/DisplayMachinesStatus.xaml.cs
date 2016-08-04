using System.Windows;

namespace videoLibrary.UserControl
{
    /// <summary>
    /// Interaction logic for DisplayMachinesStatus.xaml
    /// </summary>
    public partial class DisplayMachinesStatus
    {
        /// <summary>
        /// Dependency Property used to back the <see cref="IsPiOnline"/> property
        /// </summary>
        public static readonly DependencyProperty IsPiOnlineProperty =
            DependencyProperty.Register("IsPiOnline", 
                typeof(bool), 
                typeof(DisplayMachinesStatus), 
                new PropertyMetadata(false));

        /// <summary>
        /// Constructor
        /// </summary>
        public DisplayMachinesStatus()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boolean represents if the pi is online or not
        /// </summary>
        public bool IsPiOnline
        {
            get { return (bool)GetValue(IsPiOnlineProperty); }
            set { SetValue(IsPiOnlineProperty, value); }
        }
    }
}
