using league_mh_viewer.Models;
using league_mh_viewer.Services.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace league_mh_viewer.Services;

public class RiotApiService : IRiotApiService
{
  private readonly HttpClient _httpClient;
  private readonly IConfiguration _configuration;

  private const string AccountEndpoint =
    "https://{0}.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{1}/{2}";

  private const string MatchHistoryEndpoint =
    "https://{0}.api.riotgames.com/lol/match/v5/matches/by-puuid/{1}/ids?queue=420&start=0&count=20";

  private const string RankEndpoint =
    "https://{0}.api.riotgames.com/lol/league/v4/entries/by-puuid/{1}";

  private const string MatchDetailsEndpoint =
    "https://{0}.api.riotgames.com/lol/match/v5/matches/{1}";

  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    PropertyNameCaseInsensitive = true
  };

  public RiotApiService(HttpClient httpClient, IConfiguration configuration)
  {
    _httpClient = httpClient;
    _configuration = configuration;

    string? apiKey = _configuration["RiotApi:Key"];

    if (string.IsNullOrWhiteSpace(apiKey))
    {
      throw new InvalidOperationException("Riot API key is missing from configuration.");
    }

    if (!_httpClient.DefaultRequestHeaders.Contains("X-Riot-Token"))
    {
      _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
    }
  }

  public RiotApiService(IConfiguration configuration) : this(new HttpClient(), configuration)
  {
  }

  public RiotApiService() : this(new HttpClient(), new ConfigurationBuilder().AddJsonFile("appsettings.json").Build())
  {
  }

  public async Task<LeagueProfileItem> GetLeagueProfileAsync(string summonerName, string tag, Region region)
  {
    string encodedName = Uri.EscapeDataString(summonerName);
    string encodedTag = Uri.EscapeDataString(tag);

    string url = string.Format(
      AccountEndpoint,
      region.ApiRegion.ToString().ToLowerInvariant(),
      encodedName,
      encodedTag
    );
    
    Console.WriteLine($"Requesting Riot account with URL: {url}");

    var response = await _httpClient.GetAsync(url);

    if (!response.IsSuccessStatusCode)
    {
      throw new HttpRequestException($"Failed to get Riot account. Status: {response.StatusCode}");
    }
    
    Console.WriteLine($"Received response for Riot account: {response.StatusCode}");

    string json = await response.Content.ReadAsStringAsync();

    var profileResponse = JsonSerializer.Deserialize<LeagueProfileResponse>(json, JsonOptions);

    if (profileResponse is null)
    {
      throw new InvalidOperationException("Failed to deserialize Riot account response.");
    }

    Console.WriteLine($"Deserialized Riot account: {profileResponse.GameName}#{profileResponse.TagLine}, Puuid: {profileResponse.Puuid}");

    RankItem rank = await GetRankAsync(profileResponse.Puuid, region);
    List<MatchItem> matchHistory = await GetMatchHistoryAsync(profileResponse.Puuid, region);

    return new LeagueProfileItem(
      profileResponse.GameName,
      profileResponse.TagLine,
      profileResponse.Puuid,
      rank,
      matchHistory
    );
  }

  public async Task<RankItem> GetRankAsync(string puuid, Region region)
  {
    string url = string.Format(
      RankEndpoint,
      region.ServerRegion.ToString().ToLowerInvariant(),
      puuid
    );

    var response = await _httpClient.GetAsync(url);

    if (!response.IsSuccessStatusCode)
    {
      throw new HttpRequestException($"Failed to get rank. Status: {response.StatusCode}");
    }

    string json = await response.Content.ReadAsStringAsync();

    var rankResponse = JsonSerializer.Deserialize<List<RankResponse>>(json, JsonOptions);

    if (rankResponse is null || rankResponse.Count == 0)
    {
      return RankItem.Unranked();
    }

    var soloQueue = rankResponse.FirstOrDefault(r => r.QueueType == "RANKED_SOLO_5x5");

    if (soloQueue is null)
    {
      return RankItem.Unranked();
    }

    if (!Enum.TryParse<Tier>(soloQueue.Tier, true, out var tier))
    {
      return RankItem.Unranked();
    }

    Division? division = null;

    if (Enum.TryParse<Division>(soloQueue.Rank, true, out var parsedDivision))
    {
      division = parsedDivision;
    }

    return new RankItem(tier, division, soloQueue.LeaguePoints);
  }

  public async Task<List<MatchItem>> GetMatchHistoryAsync(string puuid, Region region)
  {
    string url = string.Format(
      MatchHistoryEndpoint,
      region.ApiRegion.ToString().ToLowerInvariant(),
      puuid
    );

    var response = await _httpClient.GetAsync(url);

    if (!response.IsSuccessStatusCode)
    {
      throw new HttpRequestException($"Failed to get match history. Status: {response.StatusCode}");
    }

    string json = await response.Content.ReadAsStringAsync();

    var matchIds = JsonSerializer.Deserialize<List<string>>(json, JsonOptions);

    if (matchIds is null)
    {
      return new List<MatchItem>();
    }

    return await GetMatchDetailsAsync(matchIds, puuid, region);
  }


  public async Task<List<MatchItem>> GetMatchDetailsAsync(List<string> matchIds, string puuid, Region region)
  {
    List<MatchItem> matchHistory = new();

    foreach (string matchId in matchIds)
    {
      string url = string.Format(
        MatchDetailsEndpoint,
        region.ApiRegion.ToString().ToLowerInvariant(),
        matchId
      );

      var response = await _httpClient.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException($"Failed to get match details for {matchId}. Status: {response.StatusCode}");
      }

      string json = await response.Content.ReadAsStringAsync();

      var matchResponse = JsonSerializer.Deserialize<MatchResponse>(json, JsonOptions);

      if (matchResponse?.Info?.Participants is null)
      {
        continue;
      }

      var searchedPlayer = matchResponse.Info.Participants.FirstOrDefault(p => p.Puuid == puuid);

      if (searchedPlayer is null)
      {
        continue;
      }

      var match = new MatchItem
      {
        GameId = matchId,
        Win = searchedPlayer.Win,
        Date = DateTimeOffset
          .FromUnixTimeMilliseconds(matchResponse.Info.GameCreation)
          .LocalDateTime
          .ToString("dd/MM/yyyy"),
        DateTime = DateTimeOffset
          .FromUnixTimeMilliseconds(matchResponse.Info.GameCreation)
          .LocalDateTime,
        Duration = TimeSpan.FromSeconds(matchResponse.Info.GameDuration),
        GameCreation = matchResponse.Info.GameCreation,
        PlayerStats = PlayerGameStats.FromParticipant(searchedPlayer),

        EnemyTeam = matchResponse.Info.Participants
          .Where(p => p.TeamId != searchedPlayer.TeamId)
          .Select(PlayerGameStats.FromParticipant)
          .ToList(),

        AllyTeam = matchResponse.Info.Participants
          .Where(p => p.TeamId == searchedPlayer.TeamId)
          .Select(PlayerGameStats.FromParticipant)
          .ToList()
      };

      matchHistory.Add(match);
    }

    return matchHistory;
  }

  public async Task<List<string>> GetMatchIdsAsync(string puuid, Region region)
  {
    string url = string.Format(
      MatchHistoryEndpoint,
      region.ApiRegion.ToString().ToLowerInvariant(),
      puuid
    );

    var response = await _httpClient.GetAsync(url);

    if (!response.IsSuccessStatusCode)
    {
      throw new HttpRequestException($"Failed to get match IDs. Status: {response.StatusCode}");
    }

    string json = await response.Content.ReadAsStringAsync();

    var matchIds = JsonSerializer.Deserialize<List<string>>(json, JsonOptions);

    return matchIds ?? new List<string>();
  }
}
