namespace Avgift
{
  public readonly struct Konstant
  {
    public double Avgift_kvartal { get; init; }
    public double Fondering_kvartal { get; init; }
    public double Moms_ut { get; init; }
    public double Vatten_rörlig_m3 { get; init; }
    public double Vatten_fast_år { get; init; }
    public double Vatten_moms { get; init; }
    public double Vatten_förbetalt_år { get; init; }
    public double El_rörlig_kWh { get; init; }
    public double El_ingår { get; init; }
    public double El_moms { get; init; }
    public double Städdag_hus { get; init; }
    public double Städdag_moms { get; init; }
    public bool Print_moms { get; init; }

    public override string ToString() => $"({Avgift_kvartal}, {Fondering_kvartal}, {Moms_ut}, {Vatten_rörlig_m3}, {Vatten_fast_år}, {Vatten_moms}, {Vatten_förbetalt_år}, {El_rörlig_kWh}, {El_ingår}, {El_moms}, {Städdag_hus}, {Städdag_moms}, {Print_moms})";
  };
}