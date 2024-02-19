namespace Avgift
{
  class Förbrukning
  {
    public double Vatten;
    public double El;
    public double Städdag;
  }

  class Avgift{
    public double Vatten;
    public double El;
    public double Städdag;
  }

  class Program
  {
    static void Main(string[] args)
    {
      var förbrukning_24q2 = new Dictionary<int, Förbrukning>()
      {
        {1, new Förbrukning {Vatten=53.269, El=0, Städdag=0 }},
        {3, new Förbrukning {Vatten=232.767, El=1, Städdag=0 }},
        {5, new Förbrukning {Vatten=101.623, El=0, Städdag=0 }},
        {7, new Förbrukning {Vatten=127.427, El=229, Städdag=0}},
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

      var förbrukning = förbrukning_24q2;


      var konstant_24q2 = new
      {
        Avgift_kvartal = 1640,
        Fondering_kvartal = 280,
        Moms_ut = 0.25,
        Vatten_rörlig_m3 = 16.12,
        Vatten_fast_år = 832.68,
        Vatten_moms = 0.25,
        Vatten_förbetalt_år = 3200,
        El_rörlig_kWh = 0.8576,
        El_ingår = 10,
        El_moms = 0.25,
        Städdag_hus = 160,
        Städdag_moms = 0.25
      };

      var konstant = konstant_24q2;


      var avgift = new Dictionary<int, Avgift>();
      foreach(var (h,f) in förbrukning)
      {
        avgift[h] = new Avgift { Vatten = 0, El = 0, Städdag = -f.Städdag*konstant.Städdag_hus };
      }


      var inbetalning_24q2 = new Dictionary<string, int[]>
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

      var inbetalning = inbetalning_24q2;

      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------|-----------+");
      Console.WriteLine("|                                   Vattenkostnad                            |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------|-----------+");
      Console.WriteLine("|Hus |Förbrukning|Rörlig     |Fast       |Total      |Förbetalt  |X Vatten   |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------|-----------+");
      foreach(var (h,f) in förbrukning)
      {
        var vatten_rörlig = f.Vatten * konstant.Vatten_rörlig_m3;
        var vatten_fast = konstant.Vatten_fast_år;
        var vatten_total = vatten_rörlig + vatten_fast;
        var vatten_extra = vatten_total - konstant.Vatten_förbetalt_år;
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|", h, f.Vatten, vatten_rörlig, vatten_fast, vatten_total, -konstant.Vatten_förbetalt_år, vatten_extra));
        avgift[h].Vatten = vatten_extra;
      }
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------|-----------+");

      Console.WriteLine();

      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|                  Elkostnad                         |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|Hus |Förbrukning|Ingår      |Justerad   |Total      |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      foreach(var (h,f) in förbrukning)
      {
        var el_justerad = f.El - konstant.El_ingår;
        if (el_justerad < 0) el_justerad = 0;
        var el_total = el_justerad * konstant.El_rörlig_kWh;
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|", h, f.El, konstant.El_ingår, el_justerad, el_total));
        avgift[h].El = el_total;
      }
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");

      Console.WriteLine();

      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|                             Att betala                         |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|Hus |Förbetalt  |X Vatten   |El         |Städdag    |Moms       |Att betala |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");
      foreach(var (h,a) in avgift)
      {
        var vatten = a.Vatten;
        var el = a.El;
        var städdag = a.Städdag;
        var moms = vatten * konstant.Vatten_moms + el * konstant.El_moms + städdag * konstant.Städdag_moms;
        var att_betala = vatten + el + städdag + moms + (konstant.Avgift_kvartal + konstant.Fondering_kvartal) * (1+konstant.Vatten_moms);
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|", h, (konstant.Avgift_kvartal + konstant.Fondering_kvartal) * (1+konstant.Vatten_moms), vatten, el, städdag, moms, att_betala));
      }
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");

      var avgift_24q2 = avgift;

      Console.WriteLine();

      Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+-------------+");
      Console.WriteLine("|                                         Bokföring                                             |");
      Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+-------------+");
      Console.WriteLine("|Ver.|Hus |Avgift     |Fondering  |X Vatten   |El         |Städdag    |Moms       |Postgiro     |");
      Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+-------------+");

      foreach (var (v, huslista) in inbetalning)
      {
        var ver_avgift_kvartal = 0;
        var ver_fondering_kvartal = 0;
        var ver_vatten = 0.0;
        var ver_el = 0.0;
        var ver_städdag = 0.0;
        var ver_moms = 0.0;
        Console.WriteLine();
        Console.WriteLine("+----+----+-----------+-----------+-----------+-----------+-----------+-----------+");
        foreach (var h in huslista)
        {
          var avgift_kvartal = konstant.Avgift_kvartal;
          ver_avgift_kvartal += avgift_kvartal;
          var fondering_kvartal = konstant.Fondering_kvartal;
          ver_fondering_kvartal += fondering_kvartal;
          var vatten = avgift[h].Vatten;
          ver_vatten += vatten;
          var el = avgift[h].El;
          ver_el += el;
          var städdag = avgift[h].Städdag;
          ver_städdag += städdag;
          var moms = avgift_kvartal * konstant.Moms_ut + fondering_kvartal * konstant.Moms_ut + vatten * konstant.Vatten_moms + el * konstant.El_moms + städdag * konstant.Städdag_moms;
          ver_moms += moms;

          Console.WriteLine(string.Format("|{0,4}|{1,4}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|{7,11:0.00}|", v, h, avgift_kvartal, fondering_kvartal, vatten, el, städdag, moms));
        }
        var ver_summa = ver_avgift_kvartal + ver_fondering_kvartal + ver_vatten + ver_el + ver_städdag + ver_moms;
        Console.WriteLine("+=========+===========+===========+===========+===========+===========+===========+=============+");
        Console.WriteLine(string.Format(" Summa     {0,11:0.00} {1,11:0.00} {2,11:0.00} {3,11:0.00} {4,11:0.00} {5,11:0.00} = {6,11:0.00}", ver_avgift_kvartal, ver_fondering_kvartal, ver_vatten, ver_el, ver_städdag, ver_moms, ver_summa));

      }
    }
  }
}