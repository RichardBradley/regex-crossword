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

    /// <summary>
    /// Enumerates the possible matches for this regex component at the given starting index and
    /// under the given constraints.
    ///
    /// Careful: the returned CharSetStrings are only valid when they are first returned from the
    /// iterator. Parts of the string are shared between successive return values. You must copy
    /// or consume the output while the iteration is proceeding.
    ///
    /// See RegexBackReference for more on this.
    /// </summary>
    /// <param name="charIdx"></param>
    /// <param name="currentConstraints"></param>
    /// <returns></returns>
    public abstract IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints);
  }
}
