using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegexCrossword.regex
{
  public class Regex : RegexAtomList
  {
    public List<RegexAtom> Atoms = new List<RegexAtom>();

    /// <summary>
    /// Parses the given pattern into a regex object
    /// </summary>
    public Regex(string pattern)
    {
      var reader = new StringReader(pattern);
      var context = new Stack<object>();
      context.Push(this);
      var capturingGroups = new List<RegexCapturingGroupChoices>();

      int ch;
      do
      {
        ch = reader.Read();
        switch (ch)
        {
          case -1:
            break;
          case '(':
            {
              var group = new RegexCapturingGroupChoices();
              capturingGroups.Add(group);
              context.Push(group);
              break;
            }
          case ')':
            {
              if (!(context.Peek() is RegexCapturingGroupChoices))
              {
                throw new ParseException(reader, "No open CapturingGroup");
              }
              var group = (RegexCapturingGroupChoices)context.Pop();
              group.FinishParse();
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
              ((RegexAtomList)context.Peek()).Add(RegexRepetitionModifier.Maybe(last));
              break;
            }
          case '*':
            {
              // modify the last element
              var last = ((RegexAtomList)context.Peek()).Pop();
              ((RegexAtomList)context.Peek()).Add(RegexRepetitionModifier.ZeroOrMore(last));
              break;
            }
          case '+':
            {
              // modify the last element
              var last = ((RegexAtomList)context.Peek()).Pop();
              ((RegexAtomList)context.Peek()).Add(RegexRepetitionModifier.OneOrMore(last));
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
            ((RegexAtomList)context.Peek()).Add(RegexCharset.AnyChar());
            break;
          case '\\':
            context.Push(new RegexBackReference());
            break;
          default:
            if (ch >= '1' && ch <= '9')
            {
              var br = (RegexBackReference) context.Pop();
              var groupNumber = (ch - '0');
              if (groupNumber > capturingGroups.Count)
              {
                // (the GeneratePossibleMatches code also relies on this, it's not just
                // a parsing thing)
                throw new ParseException(reader, "Cannot reference group before it is declared");
              }
              br.GroupNumber = groupNumber;
              br.Group = capturingGroups[groupNumber - 1];
              ((Regex)context.Peek()).Add(br);
              break;
            }
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

      FinishParse();
    }

    public Regex(params RegexAtom[] atoms)
    {
      Atoms.AddRange(atoms);
      FinishParse();
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
      var first = Atoms.First();

      var possibilitiesSeen = CharSetString.EmptySetsStringOfLength(currentConstraints.Length);
      foreach (var possibility in first.GeneratePossibleMatches(0, currentConstraints))
      {
        possibilitiesSeen.Union(possibility);
      }

      return currentConstraints.Intersect(possibilitiesSeen);
    }

    /// <summary>
    /// Set the 'Next' links on all child elements
    /// </summary>
    public void FinishParse()
    {
      // Implictly the regex must match the entire string:
      Add(new RegexEndOfLine());

      for (int i = 0; i < Atoms.Count - 1; i++)
      {
        ((RegexNonTerminalAtom) Atoms[i]).Next = Atoms[i + 1];
      }
    }

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

    public override string ToString()
    {
      // Strip off the last atom when displaying as a string, since that's a fake entry
      // we added in FinishParse
      return string.Join("", Atoms.Take(Math.Max(0, Atoms.Count - 1)));
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
             && Atoms.SequenceEqual(((Regex) obj).Atoms);
    }

    public override int GetHashCode()
    {
      return 143;
    }
  }
}
