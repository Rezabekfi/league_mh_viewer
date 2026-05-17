using System.Collections.Generic;

namespace league_mh_viewer.Models;

public class LeagueProfileItem
{
  public string Name { get; set; }
  public string Tag { get; set; }
  public string Puuid { get; set; }
  public RankItem Rank { get; set; }
  public List<MatchItem> MatchHistory { get; set; }
  public ServerRegion ServerRegion { get; set; }

  public LeagueProfileItem(
    string name,
    string tag,
    string puuid,
    RankItem rank,
    ServerRegion serverRegion,
    List<MatchItem> matchHistory)
  {
    Name = name;
    Tag = tag;
    Puuid = puuid;
    Rank = rank;
    MatchHistory = matchHistory;
    ServerRegion = serverRegion;
  }

  public override string ToString()
  {
    return $"{Name}#{Tag} - {Rank}";
  }
}
