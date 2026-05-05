using league_mh_viewer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace league_mh_viewer.Services;

public interface IRiotApiService
{
    Task<LeagueProfileItem> GetLeagueProfileAsync(string summonerName, string tag); 

    Task<List<MatchItem>> GetMatchHistoryAsync(string puuid);
}
