namespace ReillyDigital.Enumerations.Sample.Enums.Colors;

public record SecondaryColor : Color
{
	public static SecondaryColor Green => new(nameof(Green), 0x00ff00);

	public static SecondaryColor Orange => new(nameof(Orange), 0xffa500);

	public static SecondaryColor Purple => new(nameof(Purple), 0xa020f0);

	private SecondaryColor(string name, int value) : base(name, value) { }
}
