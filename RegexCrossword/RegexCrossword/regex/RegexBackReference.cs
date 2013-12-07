using System;
using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public class RegexBackReference : RegexNonTerminalAtom
  {
    public int GroupNumber;
    private RegexCapturingGroupChoices _group;

    public override IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      foreach (var previousGroupMatch in Group.PossibleMatches)
      {
        // Is the groupMatch applicable here?
        //
        // The constraints here retrospectively modify the earlier match -- any matching string
        // must match both here and before, i.e.
        // a regex like "(.)\1" applied to ".A" must become "AA"
        //
        // We bookmark the previous match, so that we can put things back how we found them
        // after iterating over this possible path.

        previousGroupMatch.Bookmark();

        var i = 0;
        foreach (var groupMatchChar in previousGroupMatch)
        {
          if (charIdx + i >= currentConstraints.Length)
          {
            goto nextPreviousGroupMatch;
          }
          try
          {
            groupMatchChar.Intersect(currentConstraints[charIdx + i]);
          }
          catch (CharSet.EmptyIntersectionException)
          {
            goto nextPreviousGroupMatch;
          }
          i++;
        }

        foreach (var remainderMatch in
          Next.GeneratePossibleMatches(
            charIdx + previousGroupMatch.Length,
            currentConstraints))
        {
          // The value returned here is valid only briefly: we will reuse the
          // same strings in the next element.
          // See comments on RegexNonTerminalAtom.GeneratePossibleMatches
          yield return previousGroupMatch.Concat(remainderMatch);
        }

      nextPreviousGroupMatch:
        previousGroupMatch.RevertToBookmark();
      }
    }

    public RegexCapturingGroupChoices Group
    {
      get
      {
        if (_group == null)
        {
          throw new Exception("Group was not set");
        }
        return _group;
      }
      set { _group = value; }
    }

    public override string ToString()
    {
      return "\\" + GroupNumber;
    }
  }
}
