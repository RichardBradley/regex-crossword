using System;
using System.Collections;
using System.Collections.Generic;

namespace RegexCrossword
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<Tuple<T,T>> ZipSameLength<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
      var firstEnum = first.GetEnumerator();
      var secondEnum = second.GetEnumerator();

      while(true)
      {
        var firstHasNext = firstEnum.MoveNext();
        if (firstHasNext != secondEnum.MoveNext())
        {
          throw new EnumerationLengthsDifferException();
        }

        if (firstHasNext)
        {
          yield return Tuple.Create(firstEnum.Current, secondEnum.Current);
        }
        else
        {
          yield break;
        }
      }
    }

    public static IEnumerable<T> Cons<T>(T head, IEnumerable<T> list)
    {
      yield return head;
      foreach (var item in list)
      {
        yield return item;
      }
    }

    public class EnumerationLengthsDifferException : Exception
    {
    }
  }
}
