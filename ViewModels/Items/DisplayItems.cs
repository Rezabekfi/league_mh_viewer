using league_mh_viewer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace league_mh_viewer.ViewModels.Items;

public class LeagueProfileDisplayItem 
{
  public LeagueProfileItem Profile { get; set; }
  public bool IsSelected { get; set; }

  public LeagueProfileDisplayItem(LeagueProfileItem profile, bool isSelected)
  {
    Profile = profile;
    IsSelected = isSelected;
  }
}

public partial class LeagueMatchDisplayItem 
{
  public MatchItem Match { get; set; }
  public bool IsExpanded { get; set; }
  public string Result => Match.Win ? "Victory" : "Defeat"; 


  public LeagueMatchDisplayItem(MatchItem match)
  {
    Match = match;
    IsExpanded = false;
  }

  public void ToggleExpanded()
  {
    IsExpanded = !IsExpanded;
  }
}
