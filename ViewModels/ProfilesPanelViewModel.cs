using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using league_mh_viewer.Models;
using league_mh_viewer.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace league_mh_viewer.ViewModels;

public partial class ProfilesPanelViewModel : ViewModelBase
{
  private readonly IRiotApiService _riotApiService;
  private readonly MatchHistoryPanelViewModel _matchHistoryViewModel;

  public ObservableCollection<ProfileCardViewModel> Profiles { get; } = new();

  [ObservableProperty]
  private string _name = "";

  [ObservableProperty]
  private string _tag = "";

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

      _matchHistoryViewModel.SetMatches(profile.MatchHistory);

      Profiles.Add(new ProfileCardViewModel((profile), OnProfileSelectionChanged));

      Console.WriteLine($"Added profile: {Name}#{Tag}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error adding profile: {ex.Message}");
    }
  }
  
  private async Task OnProfileSelectionChanged(ProfileCardViewModel changedProfile)
  {
    await RefreshGames();
  }

  public async Task RefreshGames()
  {
    var region = new Region("EUW1");
    try
    {
      List<MatchItem> allMatches = new List<MatchItem>();
      foreach (var profileItem in Profiles.Where(p => p.IsSelected))
      {
        var profile = profileItem.Profile;
        var newMatchHistory = await _riotApiService.GetMatchHistoryAsync(profile.Puuid, region);
        profile.MatchHistory = newMatchHistory;
        allMatches.AddRange(newMatchHistory);
      }
      _matchHistoryViewModel.RefreshMatches(allMatches);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error refreshing games: {ex.Message}");
    }

  }
}
