using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using league_mh_viewer.Models;
using System;
using System.Threading.Tasks;

namespace league_mh_viewer.ViewModels;

public partial class ProfileCardViewModel : ViewModelBase
{
  private readonly Func<ProfileCardViewModel, Task>? _onSelectionChanged;
  private readonly Func<ProfileCardViewModel, Task>? _removeProfileAsync;

  [ObservableProperty]
  private LeagueProfileItem _profile;

  [ObservableProperty]
  private bool _isSelected;

  public ProfileCardViewModel(LeagueProfileItem profile, bool isSelected = true,
      Func<ProfileCardViewModel, Task>? onSelectionChanged = null, Func<ProfileCardViewModel, Task>? removeProfileAsync = null)
  {
    Profile = profile;
    _onSelectionChanged = onSelectionChanged;
    _removeProfileAsync = removeProfileAsync;


    _isSelected = isSelected;
    if (!isSelected)
    {
      _ = _onSelectionChanged?.Invoke(this);
    }
  }

  [RelayCommand]
  private async Task RemoveProfile()
  {
    if (_removeProfileAsync != null)
    {
      await _removeProfileAsync(this);
    }
  }

  partial void OnIsSelectedChanged(bool value)
  {
    _ = _onSelectionChanged?.Invoke(this);
  }
}
