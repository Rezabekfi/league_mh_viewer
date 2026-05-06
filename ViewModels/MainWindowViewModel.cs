using league_mh_viewer.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace league_mh_viewer.ViewModels;


public partial class MainWindowViewModel : ViewModelBase 
{

  private readonly IRiotApiService _riotApiService;

  public MainWindowViewModel(IRiotApiService riotApiService)
  {
    _riotApiService = riotApiService;
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
    SummonerName = $"{Name}#{Tag}";
  }
}
