namespace league_mh_viewer.Models;

public class Region
{
  public ApiRegion ApiRegion { get; set; }
  public ServerRegion ServerRegion { get; set; }

  public Region(ApiRegion apiRegion, ServerRegion serverRegion)
  {
    ApiRegion = apiRegion;
    ServerRegion = serverRegion;
  }
}

public enum ApiRegion
{
  AMERICAS,
  ASIA,
  EUROPE,
  SEA
}

public enum ServerRegion
{
  NA1,
  BR1,
  LA1,
  LA2,
  OC1,
  KR,
  JP1,
  EUN1,
  EUW1,
  TR1,
  RU
}
