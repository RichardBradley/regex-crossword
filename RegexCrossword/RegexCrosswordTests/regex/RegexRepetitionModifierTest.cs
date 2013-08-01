using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword;
using RegexCrossword.regex;

namespace RegexCrosswordTests.regex
{
  [TestClass]
  public class RegexRepetitionModifierTest
  {
    [TestMethod]
    public void TestInnerMatchesOfDifferentLengths()
    {
      var reg = new Regex("(AAA|B)+");
      var rrm = (RegexRepetitionModifier)reg.Atoms[0];

      var possMatches = rrm.GeneratePossibleMatches(0, CharSetString.UnconstrainedStringOfLength(4));
      Assert.AreEqual(
        @"AAAB
BAAA
BBBB",
             String.Join("\r\n", possMatches));
    }
  }
}
