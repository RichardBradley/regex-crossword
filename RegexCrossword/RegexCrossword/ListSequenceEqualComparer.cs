using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword
{
  /// <summary>
  /// A comparer for lists which uses SequenceEqual to do a deep compare of the
  /// two given lists
  /// </summary>
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
