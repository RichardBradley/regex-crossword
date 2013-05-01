using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegexCrossword.regex
{
  public class Regex : RegexAtomList
  {
    /// <summary>
    /// Parses the given pattern into a regex object
    /// </summary>
    public Regex(string pattern)
    {
      var reader = new StringReader(pattern);
      var context = new Stack<object>();
      context.Push(this);

      int ch;
      do
      {
        ch = reader.Read();
        switch (ch)
        {
          case -1:
            return;
          case '(':
            context.Push(new RegexCapturingGroupChoices());
            break;
          case ')':
            {
              if (!(context.Peek() is RegexCapturingGroupChoices))
              {
                throw new ParseException(reader, "No open CapturingGroup");
              }
              var group = (RegexCapturingGroupChoices)context.Pop();
              ((RegexAtomList) context.Peek()).Add(group);
              break;
            }
          case '[':
            context.Push(new RegexCharset());
            break;
          case ']':
            if (!(context.Peek() is RegexCharset))
            {
              throw new ParseException(reader, "No open RegexCharset");
            }
            var charset = (RegexCharset)context.Pop();
            ((RegexAtomList)context.Peek()).Add(charset);
            break;
          case '^':
            if (context.Peek() is RegexCharset && ((RegexCharset)context.Peek()).Count == 0)
            {
              ((RegexCharset)context.Peek()).IsInclusive = false;
            }
            else
            {
              ((RegexAtomList)context.Peek()).Add(new RegexLiteralChar('^'));
            }
            break;
          case '?':
            {
              // modify the last element
              var last = ((RegexAtomList)context.Peek()).Pop();
              ((RegexAtomList)context.Peek()).Add(new RegexMaybe(last));
              break;
            }
          case '*':
            {
              // modify the last element
              var last = ((RegexAtomList)context.Peek()).Pop();
              ((RegexAtomList)context.Peek()).Add(new RegexZeroOrMore(last));
              break;
            }
          case '+':
            {
              // modify the last element
              var last = ((RegexAtomList)context.Peek()).Pop();
              ((RegexAtomList)context.Peek()).Add(new RegexOneOrMore(last));
              break;
            }
          case '|':
            {
              if (!(context.Peek() is RegexCapturingGroupChoices))
              {
                throw new ParseException(reader, "Top level | are not allowed");
              }
              ((RegexCapturingGroupChoices) context.Peek()).StartNextChoice();
              break;
            }
          case '.':
            ((RegexAtomList)context.Peek()).Add(new RegexAnyChar());
            break;
          case '\\':
            throw new NotImplementedException("TODO");
          default:
            if (ch < 'A' || ch > 'Z')
            {
              throw new ParseException(reader, "Expecting A-Z, found '" + (char)ch + "'");
            }
            if (context.Peek() is RegexCharset)
            {
              ((RegexCharset)context.Peek()).AddChar((char)ch);
            }
            else
            {
              ((RegexAtomList)context.Peek()).Add(new RegexLiteralChar((char)ch));
            }
            break;
        }
      } while (ch != -1);

      
      if (context.Peek() != this)
      {
        throw new Exception("Parse error: not all brackets closed");
      }

      // Implictly the regex must match the entire string:
      Add(new RegexEndOfLine());
    }

    public Regex(params RegexAtom[] atoms)
    {
      Atoms.AddRange(atoms);
    }

    public class ParseException : Exception
    {
      public ParseException(StringReader reader, string message)
        : base(CreateMessage(reader, message))
      {
      }

      private static string CreateMessage(StringReader reader, string message)
      {
        return string.Format("{0} just before '{1}'", message, reader.ReadToEnd());
      }
    }

    /// <summary>
    /// Given a string of possible characters, attempts to constrain the possibilities
    /// to only strings which match this regex.
    /// 
    /// Returns true if any changes were made.
    /// </summary>
    public bool AddConstraints(CharSetString currentConstraints)
    {
      var atomsEnum = Atoms.GetEnumerator();
      if (!atomsEnum.MoveNext())
      {
        throw new Exception("empty regex");
      }
      var first = atomsEnum.Current;

      var possibilitiesSeen = CharSetString.UnconstrainedStringOfLength(currentConstraints.Length);
      foreach (var possibility in first.GeneratePossibleMatches(0, currentConstraints, atomsEnum))
      {
        possibilitiesSeen.Union(possibility);
      }

      return currentConstraints.Intersect(possibilitiesSeen);
    }
  }
}
