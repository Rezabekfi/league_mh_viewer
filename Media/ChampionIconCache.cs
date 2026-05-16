using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;

namespace league_mh_viewer.Media;

public static class ChampionIconCache
{
  private static readonly Dictionary<string, Bitmap> _cache = new();

  public static Bitmap GetChampionIcon(string championName)
  {
    if (_cache.TryGetValue(championName, out var bitmap))
    {
      return bitmap;
    }

    var uri = new Uri($"avares://league_mh_viewer/Assets/champ_icons/{championName}_0.jpg");
    using var stream = AssetLoader.Open(uri);
    bitmap = new Bitmap(stream);
    _cache[championName] = bitmap;
    return bitmap;
  }
}
