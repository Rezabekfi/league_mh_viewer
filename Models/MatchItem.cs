using league_mh_viewer.Services.Responses;
using System;
using System.Collections.Generic;

namespace league_mh_viewer.Models;

public enum Role
{
  TOP,
  JNG,
  MID,
  ADC,
  SUP,
  UNKNOWN
}

public class MatchItem
{
  public bool Win { get; set; }
  public string Date { get; set; } = string.Empty;
  public TimeSpan Duration { get; set; }
  public List<PlayerGameStats> EnemyTeam { get; set; } = new();
  public List<PlayerGameStats> AllyTeam { get; set; } = new();
}

public class PlayerGameStats
{
  public string SummonerName { get; set; } = string.Empty;
  public string ChampionName { get; set; } = string.Empty;
  public int Kills { get; set; }
  public int Deaths { get; set; }
  public int Assists { get; set; }
  public int CS { get; set; }
  public Role Role { get; set; } = Role.UNKNOWN;

  public static PlayerGameStats FromParticipant(MatchParticipant participant)
  {
    return new PlayerGameStats
    {
      SummonerName = !string.IsNullOrWhiteSpace(participant.RiotIdGameName)
        ? $"{participant.RiotIdGameName}#{participant.RiotIdTagLine}"
        : participant.SummonerName,

      ChampionName = participant.ChampionName,
      Kills = participant.Kills,
      Deaths = participant.Deaths,
      Assists = participant.Assists,
      CS = participant.TotalMinionsKilled + participant.NeutralMinionsKilled,
      Role = Role.UNKNOWN
    };
  }
}
