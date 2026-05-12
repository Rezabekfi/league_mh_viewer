using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using league_mh_viewer.Models;
using league_mh_viewer.Services;
using league_mh_viewer.ViewModels.Items;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace league_mh_viewer.ViewModels;

public partial class ProfilesPanelViewModel : ViewModelBase
{
    private readonly IRiotApiService _riotApiService;
    private readonly MatchHistoryPanelViewModel _matchHistoryViewModel;

    public ObservableCollection<LeagueProfileDisplayItem> Profiles { get; } = new();

    [ObservableProperty]
    private string _name = "SummonerName";

    [ObservableProperty]
    private string _tag = "EUW";

    public ProfilesPanelViewModel(
        IRiotApiService riotApiService,
        MatchHistoryPanelViewModel matchHistoryViewModel)
    {
        _riotApiService = riotApiService;
        _matchHistoryViewModel = matchHistoryViewModel;
    }

    [RelayCommand]
    private async Task AddProfile()
    {
        var region = new Region("EUW1");

        try
        {
            var profile = await _riotApiService.GetLeagueProfileAsync(Name, Tag, region);
            var matches = await _riotApiService.GetMatchHistoryAsync(profile.Puuid, region);

            profile.MatchHistory = matches;

            _matchHistoryViewModel.SetMatches(matches);

            Profiles.Add(new LeagueProfileDisplayItem(profile, true));

            Console.WriteLine($"Added profile: {Name}#{Tag}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding profile: {ex.Message}");
        }
    }
}
