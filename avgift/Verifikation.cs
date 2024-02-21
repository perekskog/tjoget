using System.Timers;

namespace Avgift
{
  public struct Verifikation
  {
    public double Avgift { get; set; }
    public double Fondering { get; set; }
    public double Vatten { get; set; }
    public double El { get; set; }
    public double Städdag { get; set; }
    public double Moms { get; set; }
    public double Postgiro { get; set; }

    public override string ToString() => $"({Avgift}, {Fondering}, {Vatten}, {El}, {Städdag}, {Moms}, {Postgiro})";
  }
}