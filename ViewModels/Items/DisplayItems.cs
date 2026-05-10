using league_mh_viewer.Models;

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
