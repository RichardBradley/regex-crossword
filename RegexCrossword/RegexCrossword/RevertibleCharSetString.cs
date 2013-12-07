using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword
{
  /// <summary>
  /// A CharSetString that can be reverted to a previous bookmarked state.
  /// </summary>
  public class RevertibleCharSetString : CharSetString
  {
    private Stack<List<CharSet>> _bookmarks = new Stack<List<CharSet>>();

    public RevertibleCharSetString(IEnumerable<CharSet> chars)
      : base(chars)
    {
    }

    public void Bookmark()
    {
      var bookmarkedChars = this.Select(c => c.Clone()).ToList();
      _bookmarks.Push(bookmarkedChars);
    }

    public void RevertToBookmark()
    {
      var bookmarkedChars = _bookmarks.Pop();
      foreach (var pair in bookmarkedChars.ZipSameLength(this))
      {
        var old = pair.Item1;
        var curr = pair.Item2;

        curr.CopyStateFrom(old);
      }
    }

    public static RevertibleCharSetString Parse(string regexStyleFormat)
    {
      return new RevertibleCharSetString(CharSetString.Parse(regexStyleFormat));
    }
  }
}
