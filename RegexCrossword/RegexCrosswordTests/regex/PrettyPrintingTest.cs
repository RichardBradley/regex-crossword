using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword.regex;

namespace RegexCrosswordTests.regex
{
  [TestClass]
  public class PrettyPrintingTest
  {
    /// <summary>
    /// To make the pretty printer easier, any regex prints its original form
    /// when you call ToString()
    /// </summary>
    [TestMethod]
    public void TestRegexParseToStringRoundTrip()
    {
      var mitMysteryHunt2013Clues = new []
                                      {
                                        ".*H.*H.*",
                                        "(DI|NS|TH|OM)*",
                                        "F.*[AO].*[AO].*",
                                        "(O|RHH|MM)*",
                                        ".*",
                                        "C*MC(CCC|MM)*",
                                        "[^C]*[^R]*III.*",
                                        "(...?)\\1*",
                                        "([^X]|XCC)*",
                                        "(RR|HHH)*.?",
                                        "N.*X.X.X.*E",
                                        "R*D*M*",
                                        ".(C|HH)*",
                                        ".*SE.*UE.*",
                                        ".*LR.*RL.*",
                                        ".*OXR.*",
                                        "([^CEM]|EM)*",
                                        "(HHX|[^HX])*",
                                        ".*PRR.*DDC.*",
                                        ".*",
                                        "[AM]*CM(RC)*R?",
                                        "([^CM]|MM|CC)*",
                                        "(E|CR|MN)*",
                                        "P+(..)\\1.*",
                                        "[CHMNOR]*I[CHMNOR]*",
                                        "(ND|ET|IN)[^X]*",
                                        ".*G.*V.*H.*",
                                        "[CR]*",
                                        ".*XEXM*",
                                        ".*DD.*CCM.*",
                                        ".*XHCR.*X.*",
                                        ".*(.)(.)(.)(.)\\4\\3\\2\\1.*",
                                        ".*(IN|SE|HI)",
                                        "[^C]*MMM[^C]*",
                                        ".*(.)C\\1X\\1.*",
                                        "[CEIMU]*OH[AEMOR]*",
                                        "(RX|[^R])*",
                                        "[^M]*M[^M]*",
                                        "(S|MM|HHH)*"
                                      };

      foreach (var regexStr in mitMysteryHunt2013Clues)
      {
        var regex = new Regex(regexStr);
        Assert.AreEqual(regexStr, regex.ToString());
      }
    }
  }
}
