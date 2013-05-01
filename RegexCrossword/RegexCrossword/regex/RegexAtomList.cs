using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  public class RegexAtomList
  {
    public List<RegexAtom> Atoms = new List<RegexAtom>();

    public void Add(RegexAtom atom)
    {
      Atoms.Add(atom);
    }

    public RegexAtom Pop()
    {
      var last = Atoms[Atoms.Count - 1];
      Atoms.RemoveAt(Atoms.Count - 1);
      return last;
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
             && ((RegexAtomList) obj).Atoms.SequenceEqual(Atoms);
    }

    public override int GetHashCode()
    {
      return Atoms.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}]", GetType(), string.Join(", ", Atoms));
    }
  }
}
