

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
      if (MinReps == 0 && MaxReps == 1)
      {
        return string.Format("{0}?", Inner);
      }
      if (MinReps == 0 && MaxReps == null)
      {
        return string.Format("{0}*", Inner);
      }
      if (MinReps == 1 && MaxReps == null)
      {
        return string.Format("{0}+", Inner);
      }
      return string.Format("{0}{{{1},{2}}}", Inner, MinReps, MaxReps);
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
      if (MinReps == 0)
      {
        foreach (var nextMatch in Next.GeneratePossibleMatches(charIdx, currentConstraints))
        {
          yield return nextMatch;
        }
      }

      Inner.Next = new RegexEmptyMatchTerminalAtom();
      var currentInnerMatches = new List<CharSetString> { CharSetString.EmptyString() };

      for (int innerMatchCount = 1; innerMatchCount <= (MaxReps ?? Int32.MaxValue); innerMatchCount++)
      {
        var newInnerMatches = new List<CharSetString>();
        foreach (var currentInnerMatch in currentInnerMatches)
        {
          // TODO: could group the currentInnerMatches by length here, to save re-computing
          // the following repeatedly for the same arguments in some cases
          foreach (var nextInnerMatch in Inner.GeneratePossibleMatches(
            charIdx + currentInnerMatch.Length,
            currentConstraints))
          {
            var newInnerMatch = currentInnerMatch.Concat(nextInnerMatch);
            newInnerMatches.Add(newInnerMatch);

            if (innerMatchCount >= MinReps)
            {
              foreach (var nextMatch in Next.GeneratePossibleMatches(
                charIdx + newInnerMatch.Length, currentConstraints))
              {
                yield return newInnerMatch.Concat(nextMatch);
              }
            }
          }
        }
        currentInnerMatches = newInnerMatches;

        if (!currentInnerMatches.Any())
        {
          break;
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
