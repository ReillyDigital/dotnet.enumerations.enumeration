namespace ReillyDigital.Enumerations.Sample.Enums.Colors;

public record MyThemeColor : Color
{
	public static Color Border => new MyThemeColor(nameof(Border), 0xdddddd);

	public static Color PrimaryBackground => AbsoluteColor.Black;

	public static Color PrimaryFont { get; } = SecondaryColor.Green;

	public static Color SecondaryBackground { get; } = AbsoluteColor.White;

	public static Color SecondaryFont { get; } = PrimaryColor.Blue;

	private MyThemeColor(string name, int value) : base(name, value) { }
}
