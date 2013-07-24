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
    }
  }
}
