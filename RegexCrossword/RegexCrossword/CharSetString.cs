using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexCrossword
{
  /// <summary>
  /// A (mutable) string where each character is a CharSet
  /// </summary>
  public class CharSetString
  {
    public readonly CharSet[] chars;

    public CharSetString(CharSet[] chars)
    {
      this.chars = chars;
    }

    public int Length
    {
      get { return chars.Length; }
    }

    public static CharSetString UnconstrainedStringOfLength(int length)
    {
      var ret = new CharSet[length];
      for (int i = 0; i < length; i++)
      {
        ret[i] = new CharSet();
      }
      return new CharSetString(ret);
    }

    /// <summary>
    /// Epands the charsets in this string as necessary so that the given CharSetString
    /// is included in this one.
    /// </summary>
    /// <returns>this</returns>
    public CharSetString Union(CharSetString other)
    {
      if (Length != other.Length)
      {
        throw new Exception("Mismatched lengths");
      }
      for (int i = 0; i < Length; i++)
      {
        chars[i].Union(other.chars[i]);
      }
      return this;
    }

    /// <summary>
    /// Restricts the charsets in this string so that this string is no more inclusive
    /// than the given one.
    /// </summary>
    /// <returns>true if any changes were made</returns>
    public bool Intersect(CharSetString other)
    {
      if (Length != other.Length)
      {
        throw new Exception("Mismatched lengths");
      }
      var changesMade = false;
      for (int i = 0; i < Length; i++)
      {
        changesMade |= chars[i].Intersect(other.chars[i]);
      }
      return changesMade;
    }

    public CharSet this[int i]
    {
      get { return chars[i]; }
    }

    public override string ToString()
    {
      return string.Join("", (IEnumerable<CharSet>)chars);
    }
  }
}
