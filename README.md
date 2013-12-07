regex-crossword
===============

This is a C# program to solve a "regular expression crossword" by constraint propagation.

It includes a regular expression library which can enumerate possible matches to a regex given a constraint string. To save processing time, the possible matches are returned as a character set string (a minimal regex, in effect) rather than a list of strings directly:

    var regex = new Regex("X*YX*");
    var str = CharSetString.UnconstrainedStringOfLength(7);
    Assert.IsTrue(regex.AddConstraints(str));
    Assert.AreEqual("[XY][XY][XY][XY][XY][XY][XY]", str.ToString());

    str = CharSetString.Parse("..Y....");
    Assert.IsTrue(regex.AddConstraints(str));
    Assert.AreEqual("XXYXXXX", str.ToString());

MIT Mystery Hunt 2013
---------------------

This was inspired by the hexagonal regular expression crossword from the MIT Mystery Hunt 2013

* http://web.mit.edu/puzzle/www/2013/
* http://web.mit.edu/puzzle/www/2013/coinheist.com/rubik/a_regular_crossword/index.html
* http://web.mit.edu/puzzle/www/2013/coinheist.com/rubik/a_regular_crossword/grid.pdf

Reblogged at:

* http://games.slashdot.org/story/13/02/13/2346253/can-you-do-the-regular-expression-crossword
* http://www.i-programmer.info/news/144-graphics-and-games/5450-can-you-do-the-regular-expression-crossword.html
