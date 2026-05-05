using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace league_mh_viewer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  [ObservableProperty]
  private string greeting = "Hello from Avalonia + CommunityToolkit!";

  [ObservableProperty]
  private int clickCount;

  [RelayCommand]
  private void ChangeGreeting()
  {
      ClickCount++;
      Greeting = $"Button clicked {ClickCount} time(s).";
  }
}
