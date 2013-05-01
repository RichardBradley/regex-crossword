using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexCrossword.regex
{
  class RegexEndOfLine : RegexAtom
  {
    public IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints, IEnumerator<RegexAtom> nextAtomEnumerator)
    {
      throw new NotImplementedException();
    }
  }
}
