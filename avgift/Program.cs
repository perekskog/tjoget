namespace Avgift
{
  class Förbrukning
  {
    public float Vatten;
    public float El;
    public float Städdag;
  }

  class Avgift{
    public float Vatten;
    public float El;
    public float Städdag;
  }

  class Program
  {
    static void Main(string[] args)
    {
      var förbrukning_24q2 = new Dictionary<int, Förbrukning>()
      {
        {1, new Förbrukning {Vatten=53.269F, El=0, Städdag=0 }},
        {3, new Förbrukning {Vatten=232.767F, El=1, Städdag=0 }},
        {5, new Förbrukning {Vatten=101.623F, El=0, Städdag=0 }},
        {7, new Förbrukning {Vatten=127.427F, El=229, Städdag=0}},
        {9, new Förbrukning {Vatten=197.35F, El=7, Städdag=0}},
        {11, new Förbrukning {Vatten=107.06F, El=2, Städdag=0}},
        {13, new Förbrukning {Vatten=107.084F, El=1, Städdag=0}},
        {15, new Förbrukning {Vatten=172.095F, El=3, Städdag=0}},
        {17, new Förbrukning {Vatten=127.524F, El=1, Städdag=0}},
        {19, new Förbrukning {Vatten=143.459F, El=2, Städdag=0}},
        {21, new Förbrukning {Vatten=147.626F, El=3, Städdag=0}},
        {23, new Förbrukning {Vatten=165.706F, El=6, Städdag=0}},
        {25, new Förbrukning {Vatten=341.832F, El=936, Städdag=0}},
        {27, new Förbrukning {Vatten=194.535F, El=3, Städdag=0}},
        {29, new Förbrukning {Vatten=201.311F, El=447, Städdag=0}},
        {31, new Förbrukning {Vatten=177.224F, El=0, Städdag=0}},
        {33, new Förbrukning {Vatten=225.33F, El=4, Städdag=0}},
        {35, new Förbrukning {Vatten=140.22F, El=0, Städdag=0}},
        {37, new Förbrukning {Vatten=110.943F, El=2, Städdag=0}},
        {39, new Förbrukning {Vatten=130.657F, El=0, Städdag=0}}
      };

      var förbrukning = förbrukning_24q2;


      var konstant_24q2 = new
      {
        Avgift_kvartal = 1920,
        Vatten_rörlig_m3 = 16.12F,
        Vatten_fast_år = 832.68F,
        Vatten_moms = 0.25F,
        Vatten_förbetalt_år = 3200,
        El_rörlig_kWh = 0.8576F,
        El_ingår = 10,
        El_moms = 0.25F,
        Städdag_hus = 160,
        Städdag_moms = 0.25F
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

      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|                       Vattenkostnad                            |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|Hus |Förbrukning|Rörlig     |Fast       |Total      |X Vatten   |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+");
      foreach(var (h,f) in förbrukning)
      {
        var vatten_rörlig = f.Vatten * konstant.Vatten_rörlig_m3;
        var vatten_fast = konstant.Vatten_fast_år;
        var vatten_total = vatten_rörlig + vatten_fast;
        var vatten_extra = vatten_total - konstant.Vatten_förbetalt_år;
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|", h, f.Vatten, vatten_rörlig, vatten_fast, vatten_total, vatten_extra));
        avgift[h].Vatten = vatten_extra;
      }
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+");

      Console.WriteLine();

      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|                  Elkostnad                         |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      Console.WriteLine("|Hus |Förbrukning|Ingår      |Justerad   |Total      |");
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+");
      foreach(var (h,f) in förbrukning)
      {
        var el_justerad = f.El - konstant.El_ingår;
        var el_total = el_justerad * konstant.El_rörlig_kWh;
        if(el_total < 0) el_total = 0;
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|", h, f.El, konstant.El_ingår, el_justerad, el_total*(1+konstant.El_moms)));
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
        var att_betala = vatten + el + städdag + moms + konstant.Avgift_kvartal * (1+konstant.Vatten_moms);
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|{6,11:0.00}|", h, konstant.Avgift_kvartal * (1+konstant.Vatten_moms), vatten, el, städdag, moms, att_betala));
      }
      Console.WriteLine("+----+-----------+-----------+-----------+-----------+-----------+-----------+");

      var avgift_24q2 = avgift;
    }
  }
}