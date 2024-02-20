namespace Avgift
{
  public class Kalkyl
  {
    public Kostnad Vatten(Förbrukning f, Konstant c, Kostnad k)
    {
      k.Vatten_rörlig = f.Vatten * c.Vatten_rörlig_m3;
      k.Vatten_fast = c.Vatten_fast_år;
      k.Vatten_brutto = k.Vatten_rörlig + k.Vatten_fast;
      k.Vatten_netto = k.Vatten_brutto - c.Vatten_förbetalt_år;
      return k;
    }

    public Kostnad El(Förbrukning f, Konstant c, Kostnad k)
    {
      k.El_justerad = f.El - c.El_ingår;
      if (k.El_justerad < 0) k.El_justerad = 0;
      k.El_netto = k.El_justerad * c.El_rörlig_kWh;
      return k;
    }

    public Kostnad Städdag(Förbrukning f, Konstant c, Kostnad k)
    {
      k.Städdag_netto = -f.Städdag * c.Städdag_hus;
      return k;
    }

    public Kostnad Moms(Förbrukning f, Konstant c, Kostnad k)
    {
      k.Moms = k.Vatten_netto * c.Vatten_moms + k.El_netto * c.El_moms + k.Städdag_netto * c.Städdag_moms + c.Avgift_kvartal * c.Moms_ut + c.Fondering_kvartal * c.Moms_ut;
      return k;
    }

    public Kostnad Betala(Förbrukning f, Konstant c, Kostnad k)
    {
      k.AttBetala = c.Avgift_kvartal + c.Fondering_kvartal + k.Vatten_netto + k.El_netto + k.Städdag_netto + k.Moms;
      return k;
    }

    public Verifikation Verifikation(Konstant c, Kostnad k, Verifikation v)
    {
      v.Avgift += c.Avgift_kvartal;
      v.Fondering += c.Fondering_kvartal;
      v.Vatten += k.Vatten_netto;
      v.El += k.El_netto;
      v.Städdag += k.Städdag_netto;
      v.Moms += k.Moms;
      return v;
    }
  }
}