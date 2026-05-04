using Avalonia.Controls;
using league_mh_viewer.ViewModels;

namespace league_mh_viewer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
