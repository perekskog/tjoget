namespace Avgift
{
  class Program
  {
    static void Main(string[] args)
    {
      var repository = new Repository();

      var förbrukning = repository.Förbrukning("24q2");
      var konstant = repository.Konstant("24q2");
      var inbetalning = repository.Inbetalning("24q2");

      var kalkyl = new Kalkyl();
      var algoritm = new Algorithm();

      var kostnad = new Dictionary<int, Kostnad>();
      int[] hus = [1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39];
      foreach (var h in hus)
      {
        kostnad[h] = algoritm.Kostnad(kalkyl, h, förbrukning, konstant);
      }

      var verifikation = new Dictionary<string, Verifikation>();
      foreach (var (v, hl) in inbetalning)
      {
        verifikation[v] = algoritm.Verifikation(kalkyl, hl, konstant, kostnad);
      }

      var printer = new Printer();

      printer.PrintVattenkostnad(hus, förbrukning, kostnad, konstant);
      Console.WriteLine();
      printer.PrintElkostnad(hus, förbrukning, kostnad, konstant);
      Console.WriteLine();
      printer.PrintStäddag(hus, kostnad);
      Console.WriteLine();
      printer.PrintAttBetala(hus, kostnad, konstant);
      Console.WriteLine();
      printer.PrintBokföring(hus, kostnad, konstant, verifikation, inbetalning);

    }
  }
}