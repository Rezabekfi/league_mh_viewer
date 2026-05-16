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

  public void RefreshMatches(IEnumerable<MatchItem> matches)
  {
    var displayItems = ConvertToDisplayItems(matches);
    ReplaceMatches(displayItems);
  }

  public void RemoveMatchesForProfile(LeagueProfileItem profile)
  {
    var matchesToRemove = Matches.Where(m => m.Match.AllyTeam.Any(p => p.Puuid == profile.Puuid)).ToList();
    foreach (var match in matchesToRemove)
    {
      Matches.Remove(match);
    }
  }
}
