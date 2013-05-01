using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword
{
  class ListSequenceEqualComparer<T> : IEqualityComparer<List<T>>
  {
    public bool Equals(List<T> x, List<T> y)
    {
      return x.SequenceEqual(y);
    }

    public int GetHashCode(List<T> obj)
    {
      return obj.GetHashCode();
    }
  }
}
