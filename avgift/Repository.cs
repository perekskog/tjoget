namespace Avgift
{
  public readonly struct Repository
  {
    public Dictionary<int, Förbrukning> Förbrukning(string key)
    {
      switch (key)
      {
        case "23q2":
          return new Dictionary<int, Förbrukning>()
          {
            {1, new Förbrukning {Vatten=53.269, El=0, Städdag=0 }},
            {3, new Förbrukning {Vatten=232.764, El=1, Städdag=0 }},
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

        default:
          throw new Exception();
      }
    }

    public Konstant Konstant(string key)
    {
      switch (key)
      {
        case "23q2":
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
        case "23q2":
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
}