﻿using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public class RegexEndOfLine : RegexAtom
  {
    public IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      if (charIdx == currentConstraints.Length)
      {
        yield return CharSetString.EmptyString();
      }
    }

    public override bool Equals(object obj)
    {
      return GetType() == obj.GetType();
    }

    public override int GetHashCode()
    {
      return 987;
    }
  }
}
