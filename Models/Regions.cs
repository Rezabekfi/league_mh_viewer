using System;
using System.Collections.Generic;

namespace league_mh_viewer.Models;

public class Region
{
  public ApiRegion ApiRegion { get; set; }
  public ServerRegion ServerRegion { get; set; }

  public readonly Dictionary<ServerRegion, ApiRegion> ServerToApiRegionMap = new Dictionary<ServerRegion, ApiRegion>
  {
    { ServerRegion.NA1, ApiRegion.AMERICAS },
    { ServerRegion.BR1, ApiRegion.AMERICAS },
    { ServerRegion.LA1, ApiRegion.AMERICAS },
    { ServerRegion.LA2, ApiRegion.AMERICAS },
    { ServerRegion.OC1, ApiRegion.AMERICAS },
    { ServerRegion.KR, ApiRegion.ASIA },
    { ServerRegion.JP1, ApiRegion.ASIA },
    { ServerRegion.EUN1, ApiRegion.EUROPE },
    { ServerRegion.EUW1, ApiRegion.EUROPE },
    { ServerRegion.TR1, ApiRegion.EUROPE },
    { ServerRegion.RU, ApiRegion.EUROPE }
  };

  public Region(ApiRegion apiRegion, ServerRegion serverRegion)
  {
    ApiRegion = apiRegion;
    ServerRegion = serverRegion;
  }

  public Region(string apiRegion, string serverRegion)
  {
    ApiRegion = Enum.Parse<ApiRegion>(apiRegion);
    ServerRegion = Enum.Parse<ServerRegion>(serverRegion);
  }

  public Region(ServerRegion serverRegion)
  {
    ServerRegion = serverRegion;
    ApiRegion = ServerToApiRegionMap[serverRegion];
  }

  public Region(string serverRegion)
  {
    ServerRegion = Enum.Parse<ServerRegion>(serverRegion);
    ApiRegion = ServerToApiRegionMap[ServerRegion];
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
