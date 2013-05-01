using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexCrossword.regex
{
  public interface RegexAtom
  {
    bool AddPossibilitiesSeen(int idx, CharSet[] currentConstraints, CharSet[] possibilitiesSeen, IEnumerator<RegexAtom> nextEnum);
  }
}
