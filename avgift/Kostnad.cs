namespace Avgift
{
  public struct Kostnad
  {
    public double Vatten_rörlig { get; set; }
    public double Vatten_fast { get; set; }
    public double Vatten_brutto { get; set; }
    public double Vatten_netto { get; set; }
    public double El_justerad { get; set; }
    public double El_netto { get; set; }
    public double Städdag_netto { get; set; }
    public double Moms { get; set; }
    public double AttBetala { get; set; }

    public override readonly string ToString() => $"({Vatten_rörlig}, {Vatten_fast}, {Vatten_brutto}, {Vatten_netto}, {El_justerad}, {El_netto}, {Städdag_netto}, {Moms}, {AttBetala})";
    }
}