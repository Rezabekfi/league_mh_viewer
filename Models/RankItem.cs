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
  public short LP { get; set; }

  public RankItem(Tier tier, Division? division, short lp)
  {
    Tier = tier;
    Division = division;
    LP = lp;
  }
}


