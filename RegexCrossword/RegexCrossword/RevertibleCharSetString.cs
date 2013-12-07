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
    private List<CharSet> _bookmarkedChars;

    public RevertibleCharSetString(IEnumerable<CharSet> chars)
      : base(chars)
    {
    }

    public void Bookmark()
    {
      if (_bookmarkedChars != null)
      {
        throw new InvalidOperationException("The string is already bookmarked");
      }
      _bookmarkedChars = this.Select(c => c.Clone()).ToList();
    }

    public void RevertToBookmark()
    {
      if (_bookmarkedChars == null)
      {
        throw new InvalidOperationException("The string is not bookmarked");
      }
      foreach (var pair in _bookmarkedChars.ZipSameLength(this))
      {
        var old = pair.Item1;
        var curr = pair.Item2;

        curr.CopyStateFrom(old);
      }
      _bookmarkedChars = null;
    }

    public static RevertibleCharSetString Parse(string regexStyleFormat)
    {
      return new RevertibleCharSetString(CharSetString.Parse(regexStyleFormat));
    }
  }
}
