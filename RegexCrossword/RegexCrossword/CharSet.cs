using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexCrossword
{
  public class CharSet
  {
    public List<char> Chars = new List<char>();

    /// <summary>
    /// If true, then this.Chars is an inclusive list of all the chars in this CharSet.
    /// If false, then this.Chars is a list of all the chars not in this CharSet.
    /// </summary>
    public bool IsInclusive = false;

    public override bool Equals(object obj)
    {
      var that = (CharSet) obj;
      return this.IsInclusive.Equals(that.IsInclusive) && this.Chars.Equals(that.Chars);
    }

    public override int GetHashCode()
    {
      return Chars.GetHashCode() + 13 * IsInclusive.GetHashCode();
    }


    public static CharSet[] UnconstrainedStringOfLength(int length)
    {
      var ret = new CharSet[length];
      for (int i = 0; i < length; i++)
      {
        ret[i] = new CharSet();
      }
      return ret;
    }

    public static CharSet OneOf(params char[] chars)
    {
      var cs = new CharSet { IsInclusive = true };
      cs.Chars.AddRange(chars);
      return cs;
    }
  }
}
