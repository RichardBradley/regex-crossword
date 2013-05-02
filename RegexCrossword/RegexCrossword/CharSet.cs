using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword
{
  public class CharSet
  {
    public SortedSet<char> Chars = new SortedSet<char>();

    /// <summary>
    /// If true, then this.Chars is an inclusive list of all the chars in this CharSet.
    /// If false, then this.Chars is a list of all the chars not in this CharSet.
    /// </summary>
    public bool IsInclusive = false;

    public override bool Equals(object obj)
    {
      var that = (CharSet) obj;
      return IsInclusive.Equals(that.IsInclusive) && Chars.SequenceEqual(that.Chars);
    }

    public override int GetHashCode()
    {
      return Chars.GetHashCode() + 13 * IsInclusive.GetHashCode();
    }

    /// <summary>
    /// Adds a char.
    /// If the CharSet is inclusive, then this will be a new possible char;
    /// if it is exclusive, then this will be a new exclusion.
    /// </summary>
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

    /// <summary>
    /// Epands this charset as necessary so that the given CharSet is included in this one.
    /// </summary>
    /// <returns>this</returns>
    public void Union(CharSet other)
    {
      if (IsInclusive)
      {
        if (other.IsInclusive)
        {
          // [AB] + [BC] = [ABC]
          Chars.AddRange(other.Chars);
        }
        else
        {
          // [ABC] + [^CDE] = [^DE]
          var previouslyIncluded = Chars;
          Chars = new SortedSet<char>(other.Chars);
          IsInclusive = false;
          Chars.RemoveAll(previouslyIncluded);
        }
      }
      else
      {
        if (other.IsInclusive)
        {
          Chars.RemoveAll(other.Chars);
        }
        else
        {
          Chars.IntersectWith(other.Chars);
        }
      }
    }

    /// <summary>
    /// Restricts this charset so that it is no more inclusive than the given one.
    /// </summary>
    /// <returns>
    /// true if any changes were made
    /// </returns>
    /// <exception cref="EmptyIntersectionException">
    /// if the intersection would be empty
    /// </exception>
    public bool Intersect(CharSet other)
    {
      bool changesMade;
      if (IsInclusive)
      {
        if (other.IsInclusive)
        {
          var countBefore = Count;
          Chars.IntersectWith(other.Chars);
          changesMade = Count < countBefore;
        }
        else
        {
          changesMade = Chars.RemoveAll(other.Chars);
        }
      }
      else
      {
        if (other.IsInclusive)
        {
          // [^AB] ^ [BC] = [C]
          var previouslyExcluded = Chars;
          Chars = new SortedSet<char>(other.Chars);
          IsInclusive = true;
          Chars.RemoveAll(previouslyExcluded);
          changesMade = true;
        }
        else
        {
          changesMade = Chars.AddRange(other.Chars);
        }
      }

      if (IsInclusive && Chars.Count == 0)
      {
        throw new EmptyIntersectionException("No chars were found in common");
      }

      return changesMade;
    }

    public static CharSet Any()
    {
      return new CharSet();
    }

    public static CharSet OneOf(params char[] chars)
    {
      var cs = new CharSet { IsInclusive = true };
      cs.Chars.AddRange(chars);
      return cs;
    }

    public static CharSet AnyExcept(params char[] chars)
    {
      var cs = new CharSet { IsInclusive = false };
      cs.Chars.AddRange(chars);
      return cs;
    }

    public override string ToString()
    {
      if (Chars.Count == 0 && !IsInclusive)
      {
        return ".";
      }
      if (Chars.Count == 1 && IsInclusive)
      {
        return Chars.First().ToString();
      }
      return string.Format("[{0}{1}]",
                           IsInclusive ? "" : "^",
                           string.Join("", Chars));
    }

    public class EmptyIntersectionException : Exception
    {
      public EmptyIntersectionException(string message)
        : base(message)
      {
      }
    }

    public bool Contains(char ch)
    {
      return IsInclusive ? Chars.Contains(ch) : !Chars.Contains(ch);
    }

    public CharSet Clone()
    {
      return new CharSet {Chars = new SortedSet<char>(Chars), IsInclusive = IsInclusive};
    }
  }
}
