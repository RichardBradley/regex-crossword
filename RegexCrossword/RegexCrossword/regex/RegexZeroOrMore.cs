
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
    public override IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      throw new NotImplementedException();
    }
  }
}
