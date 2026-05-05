using league_mh_viewer.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
// using System.Threading.Tasks;

namespace league_mh_viewer.Services;

public class RiotApiService : IRiotApiService
{
  private readonly HttpClient _httpClient;

  public async Task<LeagueProfileItem> GetLeagueProfileAsync(string summonerName, string tag)
  {
    //placeholder return  
    return null;
  }

  public async Task<List<MatchItem>> GetMatchHistoryAsync(string puuid)
  {
    //placeholder return  
    return null;
  }
}
