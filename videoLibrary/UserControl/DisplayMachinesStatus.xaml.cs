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
        /// Dependency Property used to back the <see cref="Ip"/> property
        /// </summary>
        public static readonly DependencyProperty IpProperty =
            DependencyProperty.Register("Ip",
                typeof(string),
                typeof(DisplayMachinesStatus),
                new PropertyMetadata(""));

        /// <summary>
        /// Dependency Property used to back the <see cref="PiName"/> property
        /// </summary>
        public static readonly DependencyProperty PiNameProperty =
            DependencyProperty.Register("PiName", 
                typeof(string), 
                typeof(DisplayMachinesStatus), 
                new PropertyMetadata(""));



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

        /// <summary>
        /// String represents the physical address of the pi
        /// </summary>
        public string Ip
        {
            get { return (string)GetValue(IpProperty); }
            set { SetValue(IpProperty, value); }
        }

        /// <summary>
        /// String represents the physical name of the pi
        /// </summary>
        public string PiName
        {
            get { return (string)GetValue(PiNameProperty); }
            set { SetValue(PiNameProperty, value); }
        }
    }
}
