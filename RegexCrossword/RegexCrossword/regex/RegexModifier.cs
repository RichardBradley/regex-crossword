
namespace RegexCrossword.regex
{
  public abstract class RegexModifier : RegexAtom
  {
    public readonly RegexAtom Inner;

    protected RegexModifier(RegexAtom inner)
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
