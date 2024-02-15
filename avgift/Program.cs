using Microsoft.VisualBasic;

namespace Avgift
{
  class Förbrukning
  {
    public float Vatten;
    public float El;
    public float Städdag;
  }
  class Program
  {
    static void Main(string[] args)
    {
      var avgifter_24q2 = new Dictionary<int, Förbrukning>()
      {
        {1, new Förbrukning {Vatten=53.269F, El=0, Städdag=0 }},
        {3, new Förbrukning {Vatten=232.767F, El=1, Städdag=0 }},
        {5, new Förbrukning {Vatten=101.623F, El=0, Städdag=0 }},
        {7, new Förbrukning {Vatten=127.427F, El=229, Städdag=0}},
        {9, new Förbrukning {Vatten=197.35F, El=0, Städdag=0}},
        {11, new Förbrukning {Vatten=107.06F, El=0, Städdag=0}},
        {13, new Förbrukning {Vatten=107.084F, El=0, Städdag=0}},
        {15, new Förbrukning {Vatten=172.095F, El=0, Städdag=0}},
        {17, new Förbrukning {Vatten=127.524F, El=0, Städdag=0}},
        {19, new Förbrukning {Vatten=143.459F, El=0, Städdag=0}},
        {21, new Förbrukning {Vatten=147.626F, El=0, Städdag=0}},
        {23, new Förbrukning {Vatten=165.706F, El=0, Städdag=0}},
        {25, new Förbrukning {Vatten=341.832F, El=0, Städdag=0}},
        {27, new Förbrukning {Vatten=194.535F, El=0, Städdag=0}},
        {29, new Förbrukning {Vatten=201.311F, El=0, Städdag=0}},
        {31, new Förbrukning {Vatten=177.224F, El=0, Städdag=0}},
        {33, new Förbrukning {Vatten=225.33F, El=0, Städdag=0}},
        {35, new Förbrukning {Vatten=140.22F, El=0, Städdag=0}},
        {37, new Förbrukning {Vatten=110.943F, El=0, Städdag=0}},
        {39, new Förbrukning {Vatten=130.657F, El=0, Städdag=0}}
      };

      var konstant = new
      {
        Avgift_kvartal = 2400,
        Vatten_rörlig_m3 = 20.15F,
        Vatten_fast_år = 1040.76F,
        Vatten_moms = 25,
        Vatten_förbetalt_år = 4000,
        El_rörlig_kWh = 1.072F,
        El_ingår = 10
      };

      Console.WriteLine("Vattenkostnad");
      Console.WriteLine("|Hus |Förbrukning|Rörlig     |Fast       |Total      |Justerat   |");
      foreach(var (hus,förbrukning) in avgifter_24q2)
      {
        var vatten_rörlig = förbrukning.Vatten * konstant.Vatten_rörlig_m3;
        var vatten_fast = konstant.Vatten_fast_år;
        var vatten_total = vatten_rörlig + vatten_fast;
        var vatten_justerat = vatten_total - konstant.Vatten_förbetalt_år;
        Console.WriteLine(string.Format("|{0,4}|{1,11:0.00}|{2,11:0.00}|{3,11:0.00}|{4,11:0.00}|{5,11:0.00}|", hus, förbrukning.Vatten, vatten_rörlig, vatten_fast, vatten_total, vatten_justerat));
      }
    }
  }
}