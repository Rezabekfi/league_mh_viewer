using league_mh_viewer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace league_mh_viewer.Services;

public interface IRiotApiService
{
    Task<LeagueProfileItem> GetLeagueProfileAsync(string summonerName, string tag, Region region); 

    Task<List<MatchItem>> GetMatchHistoryAsync(string puuid, Region region);

    Task<List<MatchItem>> GetMatchDetailsAsync(List<string> matchIds, string puuid, Region region);

    Task<List<string>> GetMatchIdsAsync(string puuid, Region region);

    Task<RankItem> GetRankAsync(string puuid, Region region);
}
