using System;
using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public abstract class RegexNonTerminalAtom : RegexAtom
  {
    private RegexAtom _next;

    public RegexAtom Next
    {
      get
      {
        if (_next == null)
        {
          throw new InvalidOperationException("Next has not been set");
        }
        return _next;
      }
      set { _next = value; }
    }

    public abstract IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints);
  }
}
