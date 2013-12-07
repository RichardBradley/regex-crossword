using System;
using System.Diagnostics;
using System.IO;
using RegexCrossword.HexRegex;

namespace RegexCrossword
{
  class Program
  {
    static void Main(string[] args)
    {
      Solve(
        "MITMysteryHunt2013",
        HexRegexCrossword.MITMysteryHunt2013());
    }

    private static void Solve(string name, HexRegexCrossword puzzle)
    {
      var sw = Stopwatch.StartNew();
      try
      {
        puzzle.Solve();
      }
      catch (Exception e)
      {
        Console.WriteLine("Failed to solve: " + e);
      }

      Directory.CreateDirectory(name);
      var templ = new HexRegexCrosswordHtml { Model = puzzle };
      File.WriteAllText(
        string.Format("{0}/solved.html", name),
        templ.TransformText());

      Console.WriteLine("Done in {0}ms", sw.ElapsedMilliseconds);
    }
  }
}
