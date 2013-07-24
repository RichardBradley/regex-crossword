using System;
using System.Collections.Generic;
using RegexCrossword.regex;

namespace RegexCrossword.HexRegex
{
  /// <summary>
  /// Represents a regex crossword on a hex grid.
  /// Holds state about how far solved we are, and the latest change made
  /// </summary>
  public class HexRegexCrossword
  {
    /// <summary>
    /// The answer grid, viewed in each of the 3 directions, as a list
    /// of strings.
    /// The array is 3 X (2*n - 1)
    /// 
    /// The 0th index is the grid as horizontal strings, from top to bottom.
    /// 
    /// Rotate the grid 120deg anticlockwise to get the 1st index as horizontal
    /// strings, from top to bottm (so they run from top right to bottom left
    /// as you view the initial grid, with the first row along the bottom left edge).
    /// 
    /// Rotate the grid again to place the 2nd index along the LHS (so they run
    /// from bottom right to top left in the unrotated grid, with the first row along
    /// the bottom left edge).
    /// 
    /// Each CharSet is shared by 3 CharSetStrings
    /// </summary>
    public CharSetString[,] GridRows;

    /// <summary>
    /// The clues, viewed in each of the 3 directions, as a list
    /// of regexes.
    /// The array is 3 X (2*n - 1)
    /// </summary>
    public Regex[,] Clues;

    public List<SolveStep> SolveLog;

    /// <param name="clues">
    ///  The clues as a 3xN grid
    /// </param>
    public HexRegexCrossword(Regex[,] clues)
    {
      if (3 != clues.GetUpperBound(0) + 1 || 0 != clues.GetLowerBound(0)
          || 0 != clues.GetLowerBound(1))
      {
        throw new ArgumentException("The clue array must be 3 X rowCount");
      }
      Clues = clues;
      var rowCount = clues.GetUpperBound(1) + 1;
      var sideLength = (rowCount + 1)/2;
      GridRows = new CharSetString[3,rowCount];
      for (int i = 0; i < sideLength; i++)
      {
        GridRows[0, i] = CharSetString.UnconstrainedStringOfLength(sideLength + i);
        GridRows[0, rowCount - 1 - i] = CharSetString.UnconstrainedStringOfLength(sideLength + i);
      }
      GridRows[0, sideLength] = CharSetString.UnconstrainedStringOfLength(rowCount);

      // Diagonals
      int diagIdx = 0;
      for (int xStart = rowCount + sideLength/2 - 1; xStart >= sideLength/2; xStart--)
      {
        var chars = new List<CharSet>();
        for (int y = 0; y < rowCount; y++)
        {
          // how much to offset the row for the hex shape
          var rowStartOffsetX2 = Math.Abs(sideLength - 1 - y);
          // how far along the current horiztonal row this diagonal comes in:
          var offsetInRow = xStart - (y + rowStartOffsetX2)/2;

          if (offsetInRow >= 0 && offsetInRow < GridRows[0, y].Length)
          {
            chars.Add(GridRows[0, y][offsetInRow]);
          }
        }
        GridRows[1, diagIdx] = new CharSetString(chars);
        diagIdx++;
      }
    }

    /// <summary>
    /// Constructs the regex crossword from MIT Mystery Hunt 2013
    /// </summary>
    public static HexRegexCrossword MITMysteryHunt2013()
    {
      return new HexRegexCrossword(new Regex[3,13]
                                     {
                                       {
                                         // constant X from -6 to +6, running in +ve Y
                                         new Regex(".*G.*V.*H.*"),
                                         new Regex("[CR]*"),
                                         new Regex(".*XEXM*"),
                                         new Regex(".*DD.*CCM.*"),
                                         new Regex(".*XHCR.*X.*"),
                                         new Regex(".*(.)(.)(.)(.)\\4\\3\\2\\1.*"),
                                         new Regex(".*(IN|SE|HI)"),
                                         new Regex("[^C]*MMM[^C]*"),
                                         new Regex(".*(.)C\\1X\\1.*"),
                                         new Regex("[CEIMU]*OH[AEMOR]*"),
                                         new Regex("(RX|[^R])*"),
                                         new Regex("[^M]*M[^M]*"),
                                         new Regex("(S|MM|HHH)*")
                                       },
                                       {
                                         // constant Y from -6 to +6, running in +ve Z
                                         new Regex(".*SE.*UE.*"),
                                         new Regex(".*LR.*RL.*"),
                                         new Regex(".*OXR.*"),
                                         new Regex("([^EMC]|EM)*"),
                                         new Regex("(HHX|[^HX])*"),
                                         new Regex(".*PRR.*DDC.*"),
                                         new Regex(".*"),
                                         new Regex("[AM]*CM(RC)*R?"),
                                         new Regex("([^MC]|MM|CC)*"),
                                         new Regex("(E|CR|MN)*"),
                                         new Regex("P+(..)\\1.*"),
                                         new Regex("[CHMNOR]*I[CHMNOR]*"),
                                         new Regex("(ND|ET|IN)[^X]*")
                                       },
                                       {
                                         // constant Z, from -6 to +6, running in +ve X
                                         new Regex(".*H.*H.*"),
                                         new Regex("(DI|NS|TH|OM)*"),
                                         new Regex("F.*[AO].*[AO].*"),
                                         new Regex("(O|RHH|MM)*"),
                                         new Regex(".*"),
                                         new Regex("C*MC(CCC|MM)*"),
                                         new Regex("[^C]*[^R]*III.*"),
                                         new Regex("(...?)\\1*"),
                                         new Regex("([^X]|XCC)*"),
                                         new Regex("(RR|HHH)*.?"),
                                         new Regex("N.*X.X.X.*E"),
                                         new Regex("R*D*M*"),
                                         new Regex(".(C|HH)*")
                                       }
                                     });
    }

    /// <summary>
    /// Solves the puzzle and writes the solve log to SolveLog
    /// </summary>
    public void Solve()
    {
      SolveLog = new List<SolveStep>();
      bool changedThisCycle;
      do
      {
        changedThisCycle = false;
        for (char axis = 'X'; axis <= 'Z'; axis++)
        {
          for (int idx = -6; idx <= 6; idx++)
          {
            var clue = Clues[axis - 'X', idx + 6];
            var row = GridRows[axis - 'X', idx + 6];

            var changed = clue.AddConstraints(row);
            changedThisCycle |= changed;
            SolveLog.Add(new SolveStep
                           {
                             Changed = changed,
                             InspectedAxis = axis,
                             InspectedIdx = idx,
                             NewRowValue = row.ToString()
                           });
          }
        }
      } while (changedThisCycle);
    }

    public struct SolveStep
    {
      public char InspectedAxis;
      public int InspectedIdx;
      public string NewRowValue;
      public bool Changed;
    }
  }
}
