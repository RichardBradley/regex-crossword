using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexCrossword.regex
{
  public class RegexAnyChar : RegexAtom
  {
    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType();
    }

    public override int GetHashCode()
    {
      return 97;
    }

    public IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints, IEnumerator<RegexAtom> nextAtomEnumerator)
    {
      throw new NotImplementedException();
    }
  }
}
