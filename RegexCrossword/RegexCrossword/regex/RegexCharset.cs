
using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public class RegexCharset : RegexAtom
  {
    public CharSet CharSet;

    /// <summary>
    /// Creates a new "inclusive" charset.
    /// 
    /// If chars[] is empty, then no char will match
    /// </summary>
    /// <param name="chars"></param>
    public RegexCharset(params char[] chars)
    {
      CharSet = CharSet.OneOf(chars);
    }

    /// <summary>
    /// Adds a char.
    /// If the CharSet is inclusive, then this will be a new possible char;
    /// if it is exclusive, then this will be a new exclusion.
    /// </summary>
    public void AddChar(char ch)
    {
      CharSet.AddChar(ch);
    }

    public int Count
    {
      get { return CharSet.Count; }
    }

    public bool IsInclusive
    {
      get { return CharSet.IsInclusive; }
      set { CharSet.IsInclusive = value; }
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
             && ((RegexCharset)obj).CharSet.Equals(CharSet);
    }

    public override int GetHashCode()
    {
      return 13 * CharSet.GetHashCode();
    }

    public IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints, IEnumerator<RegexAtom> nextAtomEnumerator)
    {
      throw new System.NotImplementedException();
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}]",
        GetType(), CharSet);
    }
  }
}
