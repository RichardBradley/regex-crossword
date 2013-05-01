using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  /// <summary>
  /// Both a capturing group and a choice list, e.g. "(a|b)"
  /// </summary>
  public class RegexCapturingGroupChoices : RegexAtomList, RegexAtom, RegexParseContext
  {
    public List<List<RegexAtom>> Choices;

    public RegexCapturingGroupChoices()
    {
      Choices = new List<List<RegexAtom>> {Atoms};
    }

    public RegexCapturingGroupChoices(params List<RegexAtom>[] choices)
    {
      Choices = new List<List<RegexAtom>>(choices);
      Atoms = Choices.Last();
    }

    public void StartNextChoice()
    {
      Atoms = new List<RegexAtom>();
      Choices.Add(Atoms);
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj)
             && Choices.SequenceEqual(
               ((RegexCapturingGroupChoices) obj).Choices,
               new ListSequenceEqualComparer<RegexAtom>());
    }

    public override int GetHashCode()
    {
      return Choices.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}]",
                           GetType(),
                           string.Join("|", Choices.Select(c => string.Join(", ", c))));
    }
  }
}
