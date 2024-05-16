var repository = new Avgift.Repository();

var förbrukning = repository.Förbrukning("23q2");
var konstant = repository.Konstant("23q2-no-moms");
var inbetalning = repository.Inbetalning("23q2");

var kalkyl = new Avgift.Kalkyl();
var algoritm = new Avgift.Algorithm();

var kostnad = new Dictionary<int, Avgift.Kostnad>();
int[] hus = [1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39];
foreach (var h in hus)
{
  kostnad[h] = algoritm.Kostnad(kalkyl, h, förbrukning, konstant);
}

var verifikation = new Dictionary<string, Avgift.Verifikation>();
foreach (var (v, hl) in inbetalning)
{
  verifikation[v] = algoritm.Verifikation(kalkyl, hl, konstant, kostnad);
}

var printer = new Avgift.Printer();

printer.PrintBeräkningsvärden(konstant);
Console.WriteLine();
printer.PrintVattenkostnad(hus, förbrukning, kostnad, konstant);
Console.WriteLine();
printer.PrintElkostnad(hus, förbrukning, kostnad, konstant);
Console.WriteLine();
printer.PrintStäddag(hus, kostnad);
Console.WriteLine();
printer.PrintAttBetala(hus, kostnad, konstant);
Console.WriteLine();
printer.PrintBokföring(hus, kostnad, konstant, verifikation, inbetalning);