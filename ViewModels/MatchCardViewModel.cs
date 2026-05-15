using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using league_mh_viewer.Models;
using league_mh_viewer.Media;
using Avalonia.Media;

namespace league_mh_viewer.ViewModels;

public partial class MatchCardViewModel : ViewModelBase
{
    private readonly StreamGeometry _expandIconDown = AppIcons.ArrowDown;
    private readonly StreamGeometry _collapseIconUp = AppIcons.ArrowUp;

    [ObservableProperty]
    private MatchItem _match;

    [ObservableProperty]
    private bool _isExpanded;

    [ObservableProperty]
    private string _result;

    [ObservableProperty]
    private Avalonia.Media.StreamGeometry _expandIcon;

    public MatchCardViewModel(MatchItem match)
    {
        Match = match;
        IsExpanded = false;
        Result = match.Win ? "Victory" : "Defeat";
        ExpandIcon = _expandIconDown;
    }

    [RelayCommand]
    private void ToggleExpanded()
    {
        IsExpanded = !IsExpanded;
        ExpandIcon = IsExpanded ? _collapseIconUp : _expandIconDown;
    }
}
