using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  public class RegexLiteralChar : RegexCharset
  {
    public RegexLiteralChar(char ch)
      : base(ch)
    {
    }

    public static List<RegexAtom> ForString(string str)
    {
      return str.Select(ch => (RegexAtom)new RegexLiteralChar(ch)).ToList();
    }
  }
}
