namespace league_mh_viewer.Models;

public enum Tier
{
  Iron,
  Bronze,
  Silver,
  Gold,
  Platinum,
  Diamond,
  Master,
  Grandmaster,
  Challenger
}

public enum Division
{
  I,
  II,
  III,
  IV
}

public class RankItem
{
  public Tier Tier { get; set; }
  public Division? Division { get; set; }
  public int LP { get; set; }

  public RankItem(Tier tier, Division? division, int lp)
  {
    Tier = tier;
    Division = division;
    LP = lp;
  }

  public override string ToString()
  {
    string divisionStr = Division.HasValue ? Division.Value.ToString() : "";
    return $"{Tier} {divisionStr} {LP} LP".Trim();
  }
}


