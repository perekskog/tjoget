namespace Avgift
{
  public readonly struct Printer
  {
    public void PrintBeräkningsvärden(Konstant konstant)
    {
      Console.WriteLine("+------------------------------+");
      Console.WriteLine("|         Beräkningsvärden     |");
      Console.WriteLine("+------------------------------+");
      Console.WriteLine($"|Avgift kvartal      {konstant.Avgift_kvartal,10}|");
      Console.WriteLine($"|Fondering kvartal   {konstant.Fondering_kvartal,10}|");
      Console.WriteLine($"|Moms utgående       {konstant.Moms_ut,10}|");
      Console.WriteLine($"|Vatten rörlig m^3   {konstant.Vatten_rörlig_m3,10:F2}|");
      Console.WriteLine($"|Vatten fast år      {konstant.Vatten_fast_år,10:F2}|");
      Console.WriteLine($"|Vatten moms         {konstant.Vatten_moms,10}|");
      Console.WriteLine($"|Vatten förbetalt år {konstant.Vatten_förbetalt_år,10}|");
      Console.WriteLine($"|El rörlig kWh       {konstant.El_rörlig_kWh,10}|");
      Console.WriteLine($"|El ingår            {konstant.El_ingår,10}|");
      Console.WriteLine($"|El moms             {konstant.El_moms,10}|");
      Console.WriteLine($"|Städdag hus         {konstant.Städdag_hus,10}|");
      Console.WriteLine($"|Städdag moms        {konstant.Städdag_moms,10}|");
      Console.WriteLine("+------------------------------+");
    }

    public void PrintVattenkostnad(int[] hus, Dictionary<int, Förbrukning> förbrukning, Dictionary<int, Kostnad> kostnad, Konstant konstant)
    {
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|                                   Vattenkostnad                            |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|Hus |Förbrukning|Rörlig     |Fast       |Total      |Förbetalt  |X Vatten   |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
      foreach (var h in hus)
      {
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|", h, förbrukning[h].Vatten, kostnad[h].Vatten_rörlig, kostnad[h].Vatten_fast, kostnad[h].Vatten_brutto, -konstant.Vatten_förbetalt_år, kostnad[h].Vatten_netto));
      }
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
    }

    public void PrintElkostnad(int[] hus, Dictionary<int, Förbrukning> förbrukning, Dictionary<int, Kostnad> kostnad, Konstant konstant)
    {
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|                  Elkostnad                         |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|Hus |Förbrukning|Ingår      |Justerad   |Total      |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      foreach (var h in hus)
      {
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|", h, förbrukning[h].El, konstant.El_ingår, kostnad[h].El_justerad, kostnad[h].El_netto));
      }
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
    }

    public void PrintStäddag(int[] hus, Dictionary<int, Kostnad> kostnad)
    {
      Console.WriteLine("+----+-----------+");
      Console.WriteLine("|      Städdag   |");
      Console.WriteLine("+----+-----------+");
      Console.WriteLine("|Hus |Städdag    |");
      Console.WriteLine("+----+-----------+");
      foreach (var h in hus)
      {
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|", h, kostnad[h].Städdag_netto));
      }
      Console.WriteLine("+----+-----------+");
    }

    public void PrintAttBetala(int[] hus, Dictionary<int, Kostnad> kostnad, Konstant konstant)
    {
      if (konstant.Print_moms == true)
      {
        Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+-----------+");
        Console.WriteLine("|                             Att betala                                                 |");
        Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+-----------+");
        Console.WriteLine("|Hus |Avgift     |Fondering  |X Vatten   |El         |Städdag    |Moms       |Att betala |");
        Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+-----------+");
        foreach (var h in hus)
        {
          Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|{7,11:0.00}|", h, konstant.Avgift_kvartal, konstant.Fondering_kvartal, kostnad[h].Vatten_netto, kostnad[h].El_netto, kostnad[h].Städdag_netto, kostnad[h].Moms, kostnad[h].AttBetala));
        }
        Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
      }
      else
      {
        Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
        Console.WriteLine("|                             Att betala                                     |");
        Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
        Console.WriteLine("|Hus |Avgift     |Fondering  |X Vatten   |El         |Städdag    |Att betala |");
        Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
        foreach (var h in hus)
        {
          Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|", h, konstant.Avgift_kvartal, konstant.Fondering_kvartal, kostnad[h].Vatten_netto, kostnad[h].El_netto, kostnad[h].Städdag_netto, kostnad[h].AttBetala));
        }
        Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");

      }

    }

    public void PrintBokföring(int[] hus, Dictionary<int, Kostnad> kostnad, Konstant konstant, Dictionary<string, Verifikation> verifikation, Dictionary<string, int[]> inbetalning)
    {
      if (konstant.Print_moms == true)
      {
        Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+-------------+");
        Console.WriteLine("|                                         Bokföring                                             |");
        Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+-------------+");
        Console.WriteLine("|Ver.|Hus |Avgift     |Fondering  |X Vatten   |El         |Städdag    |Moms       |Postgiro     |");
        Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+-------------+");

        foreach (var (v, huslista) in inbetalning)
        {
          Console.WriteLine();
          Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+");
          foreach (var h in huslista)
          {
            Console.WriteLine(string.Format("|{0,4}|{1,4}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|{7,11:0.00}|", v, h, konstant.Avgift_kvartal, konstant.Fondering_kvartal, kostnad[h].Vatten_netto, kostnad[h].El_netto, kostnad[h].Städdag_netto, kostnad[h].Moms));
          }
          Console.WriteLine("+=========+===========+===========+===========+===========+===========+===========+=============+");
          Console.WriteLine(string.Format(" Summa     {0,11:0.00} {1,11:0.00} {2,11:0.00} {3,11:0.00} {4,11:0.00} {5,11:0.00} = {6,11:0.00}", verifikation[v].Avgift, verifikation[v].Fondering, verifikation[v].Vatten, verifikation[v].El, verifikation[v].Städdag, verifikation[v].Moms, verifikation[v].Postgiro));

        }
      }
      else
      {
        Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-------------+");
        Console.WriteLine("|                                         Bokföring                                 |");
        Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-------------+");
        Console.WriteLine("|Ver.|Hus |Avgift     |Fondering  |X Vatten   |El         |Städdag    |Postgiro     |");
        Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-------------+");

        foreach (var (v, huslista) in inbetalning)
        {
          Console.WriteLine();
          Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+");
          foreach (var h in huslista)
          {
            Console.WriteLine(string.Format("|{0,4}|{1,4}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|", v, h, konstant.Avgift_kvartal, konstant.Fondering_kvartal, kostnad[h].Vatten_netto, kostnad[h].El_netto, kostnad[h].Städdag_netto));
          }
          Console.WriteLine("+=========+===========+===========+===========+===========+===========+=============+");
          Console.WriteLine(string.Format(" Summa     {0,11:0.00} {1,11:0.00} {2,11:0.00} {3,11:0.00} {4,11:0.00} = {5,11:0.00}", verifikation[v].Avgift, verifikation[v].Fondering, verifikation[v].Vatten, verifikation[v].El, verifikation[v].Städdag, verifikation[v].Postgiro));

        }

      }
    }

  }
}