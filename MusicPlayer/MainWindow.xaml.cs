using System.Windows;
using MusicPlayer.ViewModels;

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainWindowViewModel();

            DataContext = _viewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            _viewModel.DisposeObservers();

            base.OnClosed(e);
        }
    }
}