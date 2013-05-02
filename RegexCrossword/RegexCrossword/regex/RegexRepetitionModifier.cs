

using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  public class RegexRepetitionModifier : RegexNonTerminalAtom
  {
    public readonly RegexNonTerminalAtom Inner;
    public readonly int MinReps;
    public readonly int? MaxReps;

    public RegexRepetitionModifier(RegexNonTerminalAtom inner, int minReps, int? maxReps)
    {
      Inner = inner;
      MinReps = minReps;
      MaxReps = maxReps;
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
             && ((RegexRepetitionModifier) obj).Inner.Equals(Inner)
             && Equals(MinReps, ((RegexRepetitionModifier)obj).MinReps)
             && Equals(MaxReps, ((RegexRepetitionModifier)obj).MaxReps);
    }

    public override int GetHashCode()
    {
      return 17*Inner.GetHashCode();
    }

    public override string ToString()
    {
      return String.Format("{0}[{1}{{{2},{3}}}]", GetType(), Inner, MinReps, MaxReps);
    }

    /// <summary>
    /// Yields each of the possible matches of the regex from this point onwards as a CharSetString.
    /// </summary>
    /// <param name="charIdx">
    /// The current index of the match in the string
    /// </param>
    /// <param name="currentConstraints">
    /// The prior constraints on any matches
    /// </param>
    public override IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      var innerMatchSoFar = CharSetString.EmptyString();
      Inner.Next = new RegexEmptyMatchTerminalAtom();

      for (int innerMatchCount = 0; innerMatchCount <= (MaxReps ?? Int32.MaxValue); innerMatchCount++)
      {
        if (innerMatchCount >= MinReps)
        {
          foreach (var nextMatch in Next.GeneratePossibleMatches(
            charIdx + innerMatchCount, currentConstraints))
          {
            yield return innerMatchSoFar.Concat(nextMatch);
          }
        }

        var innerMatch = Inner.GeneratePossibleMatches(
          charIdx + innerMatchCount,
          currentConstraints).ToList();

        if (!innerMatch.Any())
        {
          yield break;
        }
        else if (innerMatch.Count() == 1)
        {
          innerMatchSoFar = innerMatchSoFar.Concat(innerMatch.First());
        }
        else
        {
          // to support this, we'd need to return the cartesian product of innerMatches and nextMatces
          throw new Exception("multiple inner matches not supported");
        }
      }
    }

    public static RegexRepetitionModifier Maybe(RegexAtom atom)
    {
      return new RegexRepetitionModifier((RegexNonTerminalAtom) atom, 0, 1);
    }

    public static RegexRepetitionModifier ZeroOrMore(RegexAtom atom)
    {
      return new RegexRepetitionModifier((RegexNonTerminalAtom)atom, 0, null);
    }

    public static RegexRepetitionModifier OneOrMore(RegexAtom atom)
    {
      return new RegexRepetitionModifier((RegexNonTerminalAtom)atom, 1, null);
    }
  }
}
