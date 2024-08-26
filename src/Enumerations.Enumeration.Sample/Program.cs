using ReillyDigital.Enumerations.Sample.Enums.Colors;
using ReillyDigital.Enumerations.Sample.Enums.Versions;

string getMessage(Color color) => Enumeration<int>.Switch(
	color,
	(PrimaryColor.Blue, "Blue like the sky."),
	(PrimaryColor.Red, "Red like the wine."),
	(PrimaryColor.Yellow, "Yellow like the lemon."),
	(SecondaryColor.Green, "Green like the grass.")
) ?? "What a beautiful color!";

Console.WriteLine(getMessage(MyThemeColor.PrimaryFont));
Console.WriteLine();

if (
	Enumeration<int>
		.TryParse<MyThemeColor, Color>("SecondaryFont", out var secondary)
)
{
	Console.WriteLine(getMessage(secondary));
	Console.WriteLine();
}

Console.WriteLine("The known colors are:");
foreach (
	var each in
		Enumeration<int>.GetAll<AbsoluteColor, Color>()
			.Union(Enumeration<int>.GetAll<PrimaryColor, Color>())
			.Union(Enumeration<int>.GetAll<SecondaryColor, Color>())
			.Union(Enumeration<int>.GetAll<MyThemeColor, Color>())
)
{
	Console.WriteLine(each.Name);
}
Console.WriteLine();

var versionNumber = "1.0.2";
var version =
	Enumeration<MyVersionItem>
		.GetAll<MyVersions>()
		.FirstOrDefault((item) => item.Value.Version == versionNumber);
if (version is not null)
{
	version.Value.Callback();
	Console.WriteLine(
		"For more details, see the documentation at "
		+ version.Value.DocumentationUrl
	);
	Console.WriteLine();
}
