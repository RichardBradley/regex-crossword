using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword;
using RegexCrossword.HexRegex;
using RegexCrossword.regex;

namespace RegexCrosswordTests.HexRegex
{
  [TestClass]
  public class HexRegexCrosswordTest
  {
    private HexRegexCrossword _cross3 = new HexRegexCrossword(new Regex[3, 3]
                                           {
                                             {
                                               // constant X from -1 to +1, running in +ve Y
                                               new Regex("[AYZ][AYZ]"),
                                               new Regex("[AYZ][AYZ][AYZ]"),
                                               new Regex("[AYZ][AYZ]")
                                             },
                                             {
                                               // constant Y from -6 to +6, running in +ve Z
                                               new Regex("[AXZ][AXZ]"),
                                               new Regex("[AXZ][AXZ][AXZ]"),
                                               new Regex("[AXZ][AXZ]")
                                             },
                                             {
                                               // constant Z, from -6 to +6, running in +ve X
                                               new Regex("[AXY][AXY]"),
                                               new Regex("[AXY][AXY][AXY]"),
                                               new Regex("[AXY][AXY]")
                                             }
                                           });

    [TestMethod]
    public void TestCellSharing()
    {
      // label the cells, to facilitate the following tests:
      var cellLabels = new Dictionary<CharSet, char>();
      var nextLabel = 'a';
      foreach (var row in _cross3.GridRows)
      {
        foreach (var charSet in row)
        {
          if (!cellLabels.ContainsKey(charSet))
          {
            cellLabels[charSet] = nextLabel;
            nextLabel++;
          }
        }
      }

      // now check that the grid was constructed correctly:
      Assert.AreEqual("ab", string.Join("", _cross3.GridRows[0, 0].Select(ch => cellLabels[ch])));
      Assert.AreEqual("cde", string.Join("", _cross3.GridRows[0, 1].Select(ch => cellLabels[ch])));
      Assert.AreEqual("fg", string.Join("", _cross3.GridRows[0, 2].Select(ch => cellLabels[ch])));

      Assert.AreEqual("fc", string.Join("", _cross3.GridRows[1, 0].Select(ch => cellLabels[ch])));
      Assert.AreEqual("gda", string.Join("", _cross3.GridRows[1, 1].Select(ch => cellLabels[ch])));
      Assert.AreEqual("eb", string.Join("", _cross3.GridRows[1, 2].Select(ch => cellLabels[ch])));

      Assert.AreEqual("eg", string.Join("", _cross3.GridRows[2, 0].Select(ch => cellLabels[ch])));
      Assert.AreEqual("bdf", string.Join("", _cross3.GridRows[2, 1].Select(ch => cellLabels[ch])));
      Assert.AreEqual("ac", string.Join("", _cross3.GridRows[2, 2].Select(ch => cellLabels[ch])));

      Assert.AreEqual('h', nextLabel);
    }
  }
}
