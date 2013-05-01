using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public interface RegexAtom
  {
    /// <summary>
    /// Yields each of the possible matches of the regex from this point onwards as a CharSetString.
    /// </summary>
    /// <param name="charIdx">
    /// The current index of the match in the string
    /// </param>
    /// <param name="currentConstraints">
    /// The prior constraints on any matches
    /// </param>
    /// <param name="nextAtomEnumerator">
    /// An enumerator which will generate the next atoms in the regex if MoveNext() returns true
    /// </param>
    IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints, IEnumerator<RegexAtom> nextAtomEnumerator);
  }
}
