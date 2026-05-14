using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using league_mh_viewer.Models;


namespace league_mh_viewer.ViewModels;

public partial class MatchCardViewModel : ViewModelBase
{
  [ObservableProperty]
  private MatchItem _match;
  
  [ObservableProperty]
  private bool _isExpanded;

  [ObservableProperty]
  private string _result; 


  public MatchCardViewModel(MatchItem match)
  {
    Match = match;
    IsExpanded = false;
    Result = match.Win ? "Victory" : "Defeat";
  }

  [RelayCommand]
  private void ToggleExpanded()
  {
    IsExpanded = !IsExpanded;
  }

}
