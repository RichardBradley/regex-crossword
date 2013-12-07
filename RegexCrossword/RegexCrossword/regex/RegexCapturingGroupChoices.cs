using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  /// <summary>
  /// Both a capturing group and a choice list, e.g. "(a|b)"
  /// </summary>
  public class RegexCapturingGroupChoices : RegexNonTerminalAtom, RegexAtomList
  {
    public List<List<RegexAtom>> Choices;

    /// <summary>
    /// The possible strings which were matched the last time that this group
    /// was matched
    /// </summary>
    public List<RevertibleCharSetString> PossibleMatches = new List<RevertibleCharSetString>();

    public RegexCapturingGroupChoices()
    {
      Choices = new List<List<RegexAtom>> {new List<RegexAtom>()};
    }

    public RegexCapturingGroupChoices(params List<RegexAtom>[] choices)
    {
      Choices = new List<List<RegexAtom>>(choices);
      FinishParse();
    }

    public void Add(RegexAtom atom)
    {
      Choices.Last().Add(atom);
    }

    public RegexAtom Pop()
    {
      var atoms = Choices.Last();
      var last = atoms[atoms.Count - 1];
      atoms.RemoveAt(atoms.Count - 1);
      return last;
    }

    public void StartNextChoice()
    {
      Choices.Add(new List<RegexAtom>());
    }

    public override bool Equals(object obj)
    {
      return GetType() == obj.GetType()
             && Choices.SequenceEqual(
               ((RegexCapturingGroupChoices) obj).Choices,
               new ListSequenceEqualComparer<RegexAtom>());
    }

    public override int GetHashCode()
    {
      return 113;
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
      PossibleMatches.Clear();
      foreach (var choice in Choices)
      {
        var first = choice.First();
        foreach (var choiceMatchGen in first.GeneratePossibleMatches(charIdx, currentConstraints))
        {
          // choiceMatch may be modified later on (see RegexBackReference), so we promote it
          // to a RevertibleCharSetString
          var choiceMatch = new RevertibleCharSetString(choiceMatchGen);

          PossibleMatches.Add(choiceMatch);
          foreach (var remainderMatch in 
            Next.GeneratePossibleMatches(
              charIdx + choiceMatch.Length,
              currentConstraints))
          {
            yield return choiceMatch.Concat(remainderMatch);
          }
        }
      }
    }

    public override string ToString()
    {
      // Strip off the last atom when displaying each choice as a string, since that's a
      // fake entry we added in FinishParse
      Func<List<RegexAtom>, string> choiceToStr =
        c => string.Join("", c.Take(Math.Max(0, c.Count - 1)));

      return string.Format("({0})", string.Join("|", Choices.Select(choiceToStr)));
    }

    /// <summary>
    /// Set the 'Next' links on all child elements
    /// </summary>
    public void FinishParse()
    {
      foreach (var choice in Choices)
      {
        choice.Add(new RegexEmptyMatchTerminalAtom());
        for (int i = 0; i < choice.Count - 1; i++)
        {
          ((RegexNonTerminalAtom)choice[i]).Next = choice[i + 1];
        }
      }
    }
  }
}
