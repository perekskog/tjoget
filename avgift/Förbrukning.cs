namespace Avgift
{
  public readonly struct Förbrukning
  {
    public double Vatten { get; init; }
    public double El { get; init; }
    public double Städdag { get; init; }

    public override string ToString() => $"({Vatten}, {El}, {Städdag})";
  }
}