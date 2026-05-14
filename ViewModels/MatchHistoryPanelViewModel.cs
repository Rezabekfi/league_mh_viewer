using league_mh_viewer.ViewModels.Items;
using league_mh_viewer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using league_mh_viewer.ViewModels.Items;

namespace league_mh_viewer.ViewModels;

public partial class MatchHistoryPanelViewModel : ViewModelBase
{
  public ObservableCollection<LeagueMatchDisplayItem> Matches { get; } = new();

  public void SetMatches(IEnumerable<MatchItem> matches)
  {
    var displayItems = ConvertToDisplayItems(matches); 
    var sortedMatches = Matches
      .Concat(displayItems)
      .OrderByDescending(m => m.Match.GameCreation)
      .ToList();

    ReplaceMatches(sortedMatches);
  }

  private List<LeagueMatchDisplayItem> ConvertToDisplayItems(IEnumerable<MatchItem> matches)
  {
    var displayItems = matches.Select(m => new LeagueMatchDisplayItem(m)).ToList();
    return displayItems;
  }

  private void ReplaceMatches(IEnumerable<LeagueMatchDisplayItem> matches)
  {
    Matches.Clear();

    foreach (var match in matches)
    {
      Matches.Add(match);
    }
  }
}
