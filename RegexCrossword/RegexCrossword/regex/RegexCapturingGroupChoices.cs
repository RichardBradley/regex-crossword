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

    public RegexCapturingGroupChoices()
    {
      Choices = new List<List<RegexAtom>> {new List<RegexAtom>()};
    }

    public RegexCapturingGroupChoices(params List<RegexAtom>[] choices)
    {
      Choices = new List<List<RegexAtom>>(choices);
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
      foreach (var choice in Choices)
      {
        var first = choice.First();
        foreach (var choiceMatch in first.GeneratePossibleMatches(charIdx, currentConstraints))
        {
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
      return string.Format("{0}[{1}]",
                           GetType(),
                           string.Join("|", Choices.Select(c => string.Join(", ", c))));
    }

    /// <summary>
    /// Set the 'Next' links on all child elements
    /// </summary>
    public void FinishParse()
    {
      foreach (var choice in Choices)
      {
        choice.Add(new EmptyMatchTerminalRegexAtom());
        for (int i = 0; i < choice.Count - 1; i++)
        {
          ((RegexNonTerminalAtom)choice[i]).Next = choice[i + 1];
        }
      }
    }

    /// <summary>
    /// A regex atom which always matches the empty string, to terminate each child choice
    /// </summary>
    private class EmptyMatchTerminalRegexAtom : RegexAtom
    {
      public IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
      {
        return new[] { CharSetString.EmptyString() };
      }
    }
  }
}
