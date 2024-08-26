namespace ReillyDigital.Enumerations.Sample.Enums.Colors;

public abstract record Color : Enumeration<int>
{
	protected Color(string name, int value) : base(name, value) { }
}
