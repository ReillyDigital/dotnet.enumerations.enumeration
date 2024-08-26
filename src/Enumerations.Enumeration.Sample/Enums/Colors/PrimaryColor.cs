namespace ReillyDigital.Enumerations.Sample.Enums.Colors;

public sealed record PrimaryColor : Color
{
	public static PrimaryColor Blue => new(nameof(Blue), 0x0000ff);

	public static PrimaryColor Red => new(nameof(Red), 0xff0000);

	public static PrimaryColor Yellow => new(nameof(Yellow), 0xffff00);

	private PrimaryColor(string name, int value) : base(name, value) { }
}
