using league_mh_viewer.ViewModels.Items;
using league_mh_viewer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using league_mh_viewer.ViewModels;

namespace league_mh_viewer.ViewModels;

public partial class MatchHistoryPanelViewModel : ViewModelBase
{
  public ObservableCollection<MatchCardViewModel> Matches { get; } = new();

  public void SetMatches(IEnumerable<MatchItem> matches)
  {
    var displayItems = ConvertToDisplayItems(matches); 
    var sortedMatches = Matches
      .Concat(displayItems)
      .OrderByDescending(m => m.Match.GameCreation)
      .ToList();

    ReplaceMatches(sortedMatches);
  }

  private IEnumerable<MatchCardViewModel> ConvertToDisplayItems(IEnumerable<MatchItem> matches)
  {
    return matches.Select(match => new MatchCardViewModel(match));
  }

  private void ReplaceMatches(IEnumerable<MatchCardViewModel> newMatches)
  {
    Matches.Clear();
    foreach (var match in newMatches)
    {
      Matches.Add(match);
    }
  }
  
}
