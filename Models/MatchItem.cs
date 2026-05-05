using System.Collections.Generic;
using System;

namespace league_mh_viewer.Models;


public enum Role
{
  TOP,
  JNG,
  MID,
  ADC,
  SUP 
}

public class MatchItem
{
  public bool Win { get; set; }
  public string Date { get; set; }
  public TimeSpan Duration { get; set; } // this might be changed to another type depending on how the data is represented in the API response
  public List<PlayerGameStats> EnemyTeam { get; set; }
  public List<PlayerGameStats> AllyTeam { get; set; } 
}

public class PlayerGameStats
{
  public LeagueProfileItem Player { get; set; } // This might cause a circular reference (most likely is)
  public string Champion { get; set; }
  public int Kills { get; set; }
  public int Deaths { get; set; }
  public int Assists { get; set; }
  public int CS { get; set; }
  public Role Role { get; set; }
}
