using System.Collections.Generic;

namespace RegexCrossword.regex
{
  /// <summary>
  /// A regex atom which always matches the empty string
  /// </summary>
  public class RegexEmptyMatchTerminalAtom : RegexAtom
  {
    public IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      return new[] { CharSetString.EmptyString() };
    }

    public override bool Equals(object obj)
    {
      return GetType() == obj.GetType();
    }

    public override int GetHashCode()
    {
      return 937;
    }
  }
}