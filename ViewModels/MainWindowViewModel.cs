using league_mh_viewer.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace league_mh_viewer.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
    public ProfilesPanelViewModel ProfilesPanel { get; }
    public MatchHistoryPanelViewModel MatchHistory { get; }

    public IAsyncRelayCommand RefreshGamesCommand { get; }

    public MainWindowViewModel(IRiotApiService riotApiService, UserDataService userDataService)
    {
        MatchHistory = new MatchHistoryPanelViewModel();
        ProfilesPanel = new ProfilesPanelViewModel(riotApiService, MatchHistory, userDataService);

        RefreshGamesCommand = new AsyncRelayCommand(RefreshGames);
    }

    private async Task RefreshGames()
    {
        await ProfilesPanel.RefreshGames();
    }
}
  /*
  public ObservableCollection<MatchItem> MatchHistory { get; } = new ObservableCollection<MatchItem>();

  public ObservableCollection<LeagueProfileDisplayItem> Profiles { get; } = new ObservableCollection<LeagueProfileDisplayItem>();

  [RelayCommand]
  private async void AddProfile()
  {
    var region = new Region("EUW1"); // this will be replace soon
    try
    {
      var profile = await _riotApiService.GetLeagueProfileAsync(Name, Tag, region);
      var newMatchHistory = await _riotApiService.GetMatchHistoryAsync(profile.Puuid, region);
      profile.MatchHistory = newMatchHistory;

      foreach (var match in newMatchHistory)
      {
        MatchHistory.Add(match);
      }
      Profiles.Add(new LeagueProfileDisplayItem(profile, true));
      Console.WriteLine($"Added profile: {Name}#{Tag}");
    }
    catch (Exception ex)
    {
      // Handle exceptions (e.g., show error message to user)
      Console.WriteLine($"Error adding profile: {ex.Message}");
    }
  }

  [ObservableProperty]
  private string _name = "SummonerName";

  [ObservableProperty]
  private string _tag = "EUW";

  [ObservableProperty]
  private string _summonerName = "SummonerName#EUW";

  [RelayCommand]
  private void ConfirmName()
  {

  }
  */

