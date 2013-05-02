
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  public class RegexZeroOrMore : RegexModifier
  {
    public RegexZeroOrMore(RegexNonTerminalAtom inner)
      : base(inner)
    {
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

      for (int innerMatchCount = 0;; innerMatchCount++)
      {
        foreach (var nextMatch in Next.GeneratePossibleMatches(
          charIdx + innerMatchCount, currentConstraints))
        {
          yield return innerMatchSoFar.Concat(nextMatch);
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
          throw new Exception("multiple inner matches not supported");
        }
      }
    }
  }
}
