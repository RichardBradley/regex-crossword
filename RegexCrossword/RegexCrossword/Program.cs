using System;
using System.IO;
using RegexCrossword.HexRegex;

namespace RegexCrossword
{
  class Program
  {
    static void Main(string[] args)
    {
      Solve("MITMysteryHunt2013", HexRegexCrossword.MITMysteryHunt2013());
      //Console.ReadLine();
    }

    private static void Solve(string name, HexRegexCrossword puzzle)
    {
      Directory.CreateDirectory(name);

      for (int dd = 0; dd <= 2; dd++)
      {
        for (int ii = 0; ii <= 13; ii++)
        {
          puzzle.WriteHtml(
            string.Format("{0}/p-{1}-{2}.html", name,  dd, ii),
            dd,
            ii);

          return; // qq
        }
      }
    }
  }
}
