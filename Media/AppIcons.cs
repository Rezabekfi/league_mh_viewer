using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;

namespace league_mh_viewer.Media;

public static class AppIcons
{
  public static StreamGeometry ArrowDown =>
    GetIcon("arrow_down");

  public static StreamGeometry ArrowUp =>
    GetIcon("arrow_up");

  private static StreamGeometry GetIcon(string key)
  {
    if (Application.Current!.TryGetResource(key, null, out var resource)
      && resource is StreamGeometry geometry)
    {
      return geometry;
    }

    throw new InvalidOperationException($"Icon resource '{key}' was not found.");
  }
}
