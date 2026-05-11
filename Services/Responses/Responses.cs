using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace league_mh_viewer.Services.Responses;

public class LeagueProfileResponse
{
  [JsonPropertyName("puuid")]
  public string Puuid { get; set; } = string.Empty;

  [JsonPropertyName("gameName")]
  public string GameName { get; set; } = string.Empty;

  [JsonPropertyName("tagLine")]
  public string TagLine { get; set; } = string.Empty;
}

public class RankResponse
{
  [JsonPropertyName("queueType")]
  public string QueueType { get; set; } = string.Empty;

  [JsonPropertyName("tier")]
  public string Tier { get; set; } = string.Empty;

  [JsonPropertyName("rank")]
  public string Rank { get; set; } = string.Empty;

  [JsonPropertyName("leaguePoints")]
  public int LeaguePoints { get; set; }

  [JsonPropertyName("wins")]
  public int Wins { get; set; }

  [JsonPropertyName("losses")]
  public int Losses { get; set; }
}

public class MatchResponse
{
  [JsonPropertyName("metadata")]
  public MatchMetadata Metadata { get; set; } = new();

  [JsonPropertyName("info")]
  public MatchInfo Info { get; set; } = new();
}

public class MatchMetadata
{
  [JsonPropertyName("participants")]
  public List<string> Participants { get; set; } = new();
}

public class MatchInfo
{
  [JsonPropertyName("gameCreation")]
  public long GameCreation { get; set; }

  [JsonPropertyName("gameDuration")]
  public long GameDuration { get; set; }

  [JsonPropertyName("gameMode")]
  public string GameMode { get; set; } = string.Empty;

  [JsonPropertyName("participants")]
  public List<MatchParticipant> Participants { get; set; } = new();
}

public class MatchParticipant
{
  [JsonPropertyName("puuid")]
  public string Puuid { get; set; } = string.Empty;

  [JsonPropertyName("riotIdGameName")]
  public string RiotIdGameName { get; set; } = string.Empty;

  [JsonPropertyName("riotIdTagLine")]
  public string RiotIdTagLine { get; set; } = string.Empty;

  [JsonPropertyName("summonerName")]
  public string SummonerName { get; set; } = string.Empty;

  [JsonPropertyName("championName")]
  public string ChampionName { get; set; } = string.Empty;

  [JsonPropertyName("kills")]
  public int Kills { get; set; }

  [JsonPropertyName("deaths")]
  public int Deaths { get; set; }

  [JsonPropertyName("assists")]
  public int Assists { get; set; }

  [JsonPropertyName("totalMinionsKilled")]
  public int TotalMinionsKilled { get; set; }

  [JsonPropertyName("neutralMinionsKilled")]
  public int NeutralMinionsKilled { get; set; }

  [JsonPropertyName("totalDamageDealtToChampions")]
  public int TotalDamageDealtToChampions { get; set; }

  [JsonPropertyName("teamId")]
  public int TeamId { get; set; }

  [JsonPropertyName("win")]
  public bool Win { get; set; }
}
