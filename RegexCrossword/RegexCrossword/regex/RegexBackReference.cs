using System;
using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public class RegexBackReference : RegexNonTerminalAtom
  {
    private RegexCapturingGroupChoices _group;

    public override IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      foreach (var previousGroupMatch in Group.PossibleMatches)
      {
        // Is the groupMatch applicable here?
        var groupMatchHere = previousGroupMatch.Clone();
        var i = 0;
        foreach (var groupMatchChar in groupMatchHere)
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
            charIdx + groupMatchHere.Length,
            currentConstraints))
        {
          yield return groupMatchHere.Concat(remainderMatch);
        }
      }
      nextPreviousGroupMatch:;
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
  }
}
