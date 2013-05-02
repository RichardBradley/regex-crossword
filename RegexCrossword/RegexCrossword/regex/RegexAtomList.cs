using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  public interface RegexAtomList
  {
    void Add(RegexAtom regexAtom);
    RegexAtom Pop();
  }
}
