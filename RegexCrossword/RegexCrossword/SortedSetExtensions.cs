using System.Collections.Generic;

namespace RegexCrossword
{
  public static class SortedSetExtensions
  {
    public static bool AddRange<T>(this SortedSet<T> set, IEnumerable<T> items)
    {
      var changesMade = false;
      foreach (var item in items)
      {
        changesMade |= set.Add(item);
      }
      return changesMade;
    }

    public static bool RemoveAll<T>(this SortedSet<T> set, IEnumerable<T> items)
    {
      var changesMade = false;
      foreach (var item in items)
      {
        changesMade |= set.Remove(item);
      }
      return changesMade;
    }
  }
}
