

namespace RegexCrossword.regex
{
  public abstract class RegexModifier : RegexNonTerminalAtom
  {
    public readonly RegexNonTerminalAtom Inner;

    protected RegexModifier(RegexNonTerminalAtom inner)
    {
      Inner = inner;
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
             && ((RegexModifier) obj).Inner.Equals(Inner);
    }

    public override int GetHashCode()
    {
      return 17*Inner.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}]", GetType(), Inner);
    }
  }
}
