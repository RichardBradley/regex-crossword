
using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public class RegexOneOrMore : RegexModifier
  {
    public RegexOneOrMore(RegexNonTerminalAtom inner)
      : base(inner)
    {
    }

    public override IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      throw new System.NotImplementedException();
    }
  }
}
