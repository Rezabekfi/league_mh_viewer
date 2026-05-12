using league_mh_viewer.ViewModels.Items;
using league_mh_viewer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace league_mh_viewer.ViewModels;

public partial class MatchHistoryPanelViewModel : ViewModelBase
{
    public ObservableCollection<MatchItem> Matches { get; } = new();

    public void SetMatches(IEnumerable<MatchItem> matches)
    {
        Matches.Clear();

        foreach (var match in matches)
        {
            Matches.Add(match);
        }
    }
}
