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

    public bool AddPossibilitiesSeen(int idx, CharSet[] currentConstraints, CharSet[] possibilitiesSeen, IEnumerator<RegexAtom> nextEnum)
    {
      if (idx)
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}]", GetType(), Ch);
    }
  }
}
