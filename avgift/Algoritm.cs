namespace Avgift
{
  public class Algorithm
  {
    public Kostnad Kostnad(Kalkyl kalkyl, int h, Dictionary<int, Förbrukning> förbrukning, Konstant konstant)
    {
      var kostnad = kalkyl.Vatten(förbrukning[h], konstant, new Kostnad());
      kostnad = kalkyl.El(förbrukning[h], konstant, kostnad);
      kostnad = kalkyl.Städdag(förbrukning[h], konstant, kostnad);
      kostnad = kalkyl.Moms(förbrukning[h], konstant, kostnad);
      kostnad = kalkyl.Betala(förbrukning[h], konstant, kostnad);
      return kostnad;
    }

    public Verifikation Verifikation(Kalkyl kalkyl, int[] hl, Konstant konstant, Dictionary<int, Kostnad> kostnad)
    {
      var verifikation = new Verifikation();
      foreach (var h in hl)
      {
        verifikation = kalkyl.Verifikation(konstant, kostnad[h], verifikation);
      }
      verifikation.Postgiro = verifikation.Avgift + verifikation.Fondering + verifikation.Vatten + verifikation.El + verifikation.Städdag + verifikation.Moms;
      return verifikation;
    }
  }
}