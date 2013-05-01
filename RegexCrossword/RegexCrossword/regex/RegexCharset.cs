using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexCrossword.regex
{
  public class RegexCharset : RegexAtom
  {
    public bool IsInverse = false;
    public List<char> Chars = new List<char>();

    public RegexCharset(params char[] chars)
    {
      Chars.AddRange(chars);
    }

    public void AddChar(char ch)
    {
      Chars.Add(ch);
    }

    public int Count
    {
      get
      {
        return Chars.Count;
      }
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
             && ((RegexCharset)obj).IsInverse == IsInverse
             && ((RegexCharset)obj).Chars.SequenceEqual(Chars);
    }

    public override int GetHashCode()
    {
      return 13 * IsInverse.GetHashCode() + Chars.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}{2}]",
        GetType(), IsInverse ? "^" : "", string.Join("", Chars));
    }
  }
}
