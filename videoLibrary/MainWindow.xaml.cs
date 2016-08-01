using System;
using videoLibrary.ViewModel;

namespace videoLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            viewModel.PlayRequested += ViewModelOnPlayRequested;

            viewModel.StopRequested += ViewModelOnStopRequested;
        }

        private void ViewModelOnStopRequested(object sender, EventArgs eventArgs)
        {
            VideoPlayer.Close();
        }

        private void ViewModelOnPlayRequested(object sender, EventArgs eventArgs)
        {
            VideoPlayer.Play();
        }
    }
}
