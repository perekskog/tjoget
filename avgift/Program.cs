using System.Diagnostics.Contracts;

namespace Avgift
{
  class Förbrukning
  {
    public double Vatten;
    public double El;
    public double Städdag;
  }

  class Konstant
  {
    public double Avgift_kvartal;
    public double Fondering_kvartal;
    public double Moms_ut;
    public double Vatten_rörlig_m3;
    public double Vatten_fast_år;
    public double Vatten_moms;
    public double Vatten_förbetalt_år;
    public double El_rörlig_kWh;
    public double El_ingår;
    public double El_moms;
    public double Städdag_hus;
    public double Städdag_moms;
  };

  class Kostnad
  {
    public double Vatten_rörlig;
    public double Vatten_fast;
    public double Vatten_brutto;
    public double Vatten_netto;
    public double El_justerad;
    public double El_netto;
    public double Städdag_netto;
    public double Moms;
    public double AttBetala;
  }

  class Verifikation
  {
    public double Avgift;
    public double Fondering;
    public double Vatten;
    public double El;
    public double Städdag;
    public double Moms;
    public double Postgiro;
  }

  class Kalkyl
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

  class Repository
  {
    public Dictionary<int, Förbrukning> Förbrukning(string key)
    {
      switch (key)
      {
        case "24q2":
          return new Dictionary<int, Förbrukning>()
          {
            {1, new Förbrukning {Vatten=53.269, El=0, Städdag=0 }},
            {3, new Förbrukning {Vatten=232.764, El=1, Städdag=0 }},
            {5, new Förbrukning {Vatten=101.623, El=0, Städdag=0 }},
            {7, new Förbrukning {Vatten=127.427, El=229, Städdag=1}},
            {9, new Förbrukning {Vatten=197.35, El=7, Städdag=0}},
            {11, new Förbrukning {Vatten=107.06, El=2, Städdag=0}},
            {13, new Förbrukning {Vatten=107.084, El=1, Städdag=0}},
            {15, new Förbrukning {Vatten=172.095, El=3, Städdag=0}},
            {17, new Förbrukning {Vatten=127.524, El=1, Städdag=0}},
            {19, new Förbrukning {Vatten=143.459, El=2, Städdag=0}},
            {21, new Förbrukning {Vatten=147.626, El=3, Städdag=0}},
            {23, new Förbrukning {Vatten=165.706, El=6, Städdag=0}},
            {25, new Förbrukning {Vatten=341.832, El=936, Städdag=0}},
            {27, new Förbrukning {Vatten=194.535, El=3, Städdag=0}},
            {29, new Förbrukning {Vatten=201.311, El=447, Städdag=0}},
            {31, new Förbrukning {Vatten=177.224, El=0, Städdag=0}},
            {33, new Förbrukning {Vatten=225.33, El=4, Städdag=0}},
            {35, new Förbrukning {Vatten=140.22, El=0, Städdag=0}},
            {37, new Förbrukning {Vatten=110.943, El=2, Städdag=0}},
            {39, new Förbrukning {Vatten=130.657, El=0, Städdag=0}}
          };

        default:
          throw new Exception();
      }
    }

    public Konstant Konstant(string key)
    {
      switch (key)
      {
        case "24q2":
          return new Konstant
          {
            Avgift_kvartal = 1640,
            Fondering_kvartal = 280,
            Moms_ut = 0.25,
            Vatten_rörlig_m3 = 16.12,
            Vatten_fast_år = 832.6112,
            Vatten_moms = 0.25,
            Vatten_förbetalt_år = 3200,
            El_rörlig_kWh = 0.8576,
            El_ingår = 10,
            El_moms = 0.25,
            Städdag_hus = 160,
            Städdag_moms = 0.25
          };
        default:
          throw new Exception();
      }
    }

    public Dictionary<string, int[]> Inbetalning(string key)
    {
      switch (key)
      {
        case "24q2":
          return new Dictionary<string, int[]>
          {
            { "A34", [31]},
            { "A35", [1, 5, 13, 15, 21, 27, 29]},
            { "A36", [9, 35]},
            { "A37", [3, 7, 33]},
            { "A38", [19, 39]},
            { "A39", [11, 23, 37]},
            { "A41", [25]},
            { "A42", [17]}
          };
        default:
          throw new Exception();
      }
    }
  }

  class Program
  {

    static void Main(string[] args)
    {
      var repository = new Repository();

      var förbrukning = repository.Förbrukning("24q2");
      var konstant = repository.Konstant("24q2");
      var inbetalning = repository.Inbetalning("24q2");

      var kalkyl = new Kalkyl();

      var kostnad = new Dictionary<int, Kostnad>();
      int[] hus = [1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39];
      foreach (var h in hus)
      {
        kostnad[h] = kalkyl.Vatten(förbrukning[h], konstant, new Kostnad());
        kostnad[h] = kalkyl.El(förbrukning[h], konstant, kostnad[h]);
        kostnad[h] = kalkyl.Städdag(förbrukning[h], konstant, kostnad[h]);
        kostnad[h] = kalkyl.Moms(förbrukning[h], konstant, kostnad[h]);
        kostnad[h] = kalkyl.Betala(förbrukning[h], konstant, kostnad[h]);
      }

      var verifikation = new Dictionary<string, Verifikation>();
      foreach (var (v, hl) in inbetalning)
      {
        verifikation[v] = new Verifikation();
        foreach (var h in hl)
        {
          verifikation[v] = kalkyl.Verifikation(konstant, kostnad[h], verifikation[v]);
        }
        verifikation[v].Postgiro = verifikation[v].Avgift + verifikation[v].Fondering + verifikation[v].Vatten + verifikation[v].El + verifikation[v].Städdag + verifikation[v].Moms;
      }




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


      Console.WriteLine();

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

      Console.WriteLine();

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

      Console.WriteLine();

      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|                             Att betala                                                 |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|Hus |Avgift     |Fondering  |X Vatten   |El         |Städdag    |Moms       |Att betala |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+-----------+");
      foreach (var h in hus)
      {
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|{7,11:0.00}|", h, konstant.Avgift_kvartal, konstant.Fondering_kvartal, kostnad[h].Vatten_netto, kostnad[h].El_netto, kostnad[h].Städdag_netto, kostnad[h].Moms, kostnad[h].AttBetala));
      }
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+-----------+");

      Console.WriteLine();

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
  }
}