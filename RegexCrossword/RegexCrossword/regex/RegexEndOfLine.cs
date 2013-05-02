using System.Collections.Generic;

namespace RegexCrossword.regex
{
  class RegexEndOfLine : RegexAtom
  {
    public IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      if (charIdx == currentConstraints.Length)
      {
        yield return CharSetString.EmptyString();
      }
    }
  }
}
