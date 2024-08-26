namespace ReillyDigital.Enumerations.Sample.Enums.Colors;

public sealed record AbsoluteColor : Color
{
	public static AbsoluteColor Black => new(nameof(Black), 0x000000);

	public static AbsoluteColor White => new(nameof(White), 0xffffff);

	private AbsoluteColor(string name, int value) : base(name, value) { }
}
