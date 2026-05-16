using league_mh_viewer.Services.Responses;
using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using league_mh_viewer.Media;

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
  public string GameId { get; set; } = string.Empty;
  public bool Win { get; set; }
  public string Date { get; set; } = string.Empty;
  public DateTime DateTime { get; set; }
  public long GameCreation { get; set; }
  public TimeSpan Duration { get; set; }
  public PlayerGameStats PlayerStats { get; set; } = new();
  public List<PlayerGameStats> EnemyTeam { get; set; } = new();
  public List<PlayerGameStats> AllyTeam { get; set; } = new();
}

public class PlayerGameStats
{
  public string SummonerName { get; set; } = string.Empty;
  public string ChampionName { get; set; } = string.Empty;
  public string Puuid { get; set; } = string.Empty;
  public int Kills { get; set; }
  public int Deaths { get; set; }
  public int Assists { get; set; }
  public int CS { get; set; }
  public Role Role { get; set; } = Role.UNKNOWN;
  public string ScoreText => $"{Kills}/{Deaths}/{Assists}";

  public Bitmap? ChampionIcon { get; set; }


  public static PlayerGameStats FromParticipant(MatchParticipant participant)
  {
    return new PlayerGameStats
    {
      SummonerName = !string.IsNullOrWhiteSpace(participant.RiotIdGameName)
        ? $"{participant.RiotIdGameName}#{participant.RiotIdTagLine}"
        : participant.SummonerName,
      Puuid = participant.Puuid,
      ChampionName = participant.ChampionName,
      Kills = participant.Kills,
      Deaths = participant.Deaths,
      Assists = participant.Assists,
      CS = participant.TotalMinionsKilled + participant.NeutralMinionsKilled,
      Role = Role.UNKNOWN,
      ChampionIcon = ChampionIconCache.GetChampionIcon(participant.ChampionName)
    };
  }
}
