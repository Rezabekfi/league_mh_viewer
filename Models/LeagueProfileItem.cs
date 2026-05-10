using System.Collections.Generic;
using league_mh_viewer.Models;

namespace league_mh_viewer.Models;


public class LeagueProfileItem
{
  public string Name { get; set; }
  public RankItem Rank { get; set; }
  public List<MatchItem> MatchHistory { get; set; }

  public LeagueProfileItem(string name, RankItem rank, List<MatchItem> matchHistory)
  {
    Name = name;
    Rank = rank;
    MatchHistory = matchHistory;
  }

  public override string ToString()
  {
    return $"{Name} - {Rank}";
  }
}


