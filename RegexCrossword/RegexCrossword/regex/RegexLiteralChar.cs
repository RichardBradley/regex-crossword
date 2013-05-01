using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  public class RegexLiteralChar : RegexAtom
  {
    public readonly char Ch;

    public RegexLiteralChar(char ch)
    {
      Ch = ch;
    }

    public static List<RegexAtom> ForString(string str)
    {
      return str.Select(ch => (RegexAtom)new RegexLiteralChar(ch)).ToList();
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
        && Ch.Equals(((RegexLiteralChar)obj).Ch);
    }

    public override int GetHashCode()
    {
      return Ch.GetHashCode();
    }

    public IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints, IEnumerator<RegexAtom> nextAtomEnumerator)
    {
      throw new System.NotImplementedException();
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}]", GetType(), Ch);
    }
  }
}
