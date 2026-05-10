using league_mh_viewer.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using league_mh_viewer.Models;
using System.Collections.Generic;
using System;

namespace league_mh_viewer.ViewModels;


public partial class MainWindowViewModel : ViewModelBase 
{

  private readonly IRiotApiService _riotApiService;

  public MainWindowViewModel(IRiotApiService riotApiService)
  {
    _riotApiService = riotApiService;
  }

  // public ObservableCollection<MatchItem> MatchHistory { get; } = new ObservableCollection<MatchItem>();
  
  
  public ObservableCollection<MatchItem> MatchHistory { get; } = new () {
    new MatchItem {
      Win = true,
      Date = "01/01/2024",
      Duration = TimeSpan.FromMinutes(30),
      EnemyTeam = new List<PlayerGameStats>(),
      AllyTeam = new List<PlayerGameStats>()
    },
    new MatchItem {
      Win = false,
      Date = "02/01/2024",
      Duration = TimeSpan.FromMinutes(25),
      EnemyTeam = new List<PlayerGameStats>(),
      AllyTeam = new List<PlayerGameStats>()
    }
  };
  // public MatchItem(){
  //   Win = true;
  //   Date = "01/01/2024";
  //   Duration = TimeSpan.FromMinutes(30);
  //   EnemyTeam = new List<PlayerGameStats>();
  //   AllyTeam = new List<PlayerGameStats>();
  // }

  
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
