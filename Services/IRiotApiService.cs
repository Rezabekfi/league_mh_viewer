using league_mh_viewer.Models;

namespace league_mh_viewer.Services;

public interface IRiotApiService
{
    Task<LeagueProfileItem> GetLeagueProfileAsync(string summonerName); // might need to change with the summoner name + tag

    Task<List<MatchItem>> GetMatchHistoryAsync(string puuid);
}
