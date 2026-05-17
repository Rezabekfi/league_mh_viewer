using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using league_mh_viewer.Models;
using System;
using System.Threading.Tasks;

namespace league_mh_viewer.ViewModels;

public partial class ProfileCardViewModel : ViewModelBase
{
  private readonly Func<ProfileCardViewModel, Task>? _onSelectionChanged;

  [ObservableProperty]
  private LeagueProfileItem _profile;

  [ObservableProperty]
  private bool _isSelected;

  public ProfileCardViewModel(LeagueProfileItem profile, bool isSelected = true,
      Func<ProfileCardViewModel, Task>? onSelectionChanged = null)
  {
    Profile = profile;
    _onSelectionChanged = onSelectionChanged;

    _isSelected = isSelected;
    if (!isSelected)
    {
      _ = _onSelectionChanged?.Invoke(this);
    }
  }

  partial void OnIsSelectedChanged(bool value)
  {
    _ = _onSelectionChanged?.Invoke(this);
  }
}
