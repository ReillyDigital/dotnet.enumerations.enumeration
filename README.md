# ReillyDigital.Enumerations.Enumeration

An enumeration library for .NET for creating an enum of anything.

## Usage

### Simple Enumeration Value Types

Define enumeration classes:
```csharp
public abstract record Color : Enumeration<int>
{
	protected Color(string name, int value) : base(name, value) { }
}

public sealed record AbsoluteColor : Color
{
	public static AbsoluteColor Black => new(nameof(Black), 0x000000);

	public static AbsoluteColor White => new(nameof(White), 0xffffff);

	private AbsoluteColor(string name, int value) : base(name, value) { }
}

public sealed record PrimaryColor : Color
{
	public static PrimaryColor Blue => new(nameof(Blue), 0x0000ff);

	public static PrimaryColor Red => new(nameof(Red), 0xff0000);

	public static PrimaryColor Yellow => new(nameof(Yellow), 0xffff00);

	private PrimaryColor(string name, int value) : base(name, value) { }
}

public record SecondaryColor : Color
{
	public static SecondaryColor Green => new(nameof(Green), 0x00ff00);

	public static SecondaryColor Orange => new(nameof(Orange), 0xffa500);

	public static SecondaryColor Purple => new(nameof(Purple), 0xa020f0);

	private SecondaryColor(string name, int value) : base(name, value) { }
}

public record MyThemeColor : Color
{
	public static Color Border => new MyThemeColor(nameof(Border), 0xdddddd);

	public static Color PrimaryBackground => AbsoluteColor.Black;

	public static Color PrimaryFont { get; } = SecondaryColor.Green;

	public static Color SecondaryBackground { get; } = AbsoluteColor.White;

	public static Color SecondaryFont { get; } = PrimaryColor.Blue;

	private MyThemeColor(string name, int value) : base(name, value) { }
}

```

Access the enumeration items similar to how it would be done with a native `enum`:
```csharp
var primaryFont = MyThemeColor.PrimaryFont;
```

Switch on an enumeration value:
```csharp
var message = Enumeration<int>.Switch<Color, string>(
	primaryFont,
	(PrimaryColor.Blue, "Blue like the sky."),
	(PrimaryColor.Red, "Red like the wine."),
	(PrimaryColor.Yellow, "Yellow like the lemon."),
	(SecondaryColor.Green, "Green like the grass.")
)
Console.WriteLine(message);
// will output "Green like the grass."

var handler = Enumeration<int>.Switch<Color, Action>(
	primaryFont,
	(PrimaryColor.Blue, () => { /* Handle something for blue */ }),
	(PrimaryColor.Red, () => { /* Handle something for red */ })
) ?? (() => { /* Handle everything else */ });
handler();
```

Parse an enumeration item:
```csharp
var font = AbsoluteColor.Black;
var configuredFontName = "SecondaryFont";
if (
	Enumeration<int>
		.TryParse<MyThemeColor, Color>(configuredFontName, out var configuredFont)
)
{
	font = configuredFont;
}

```

Get all items for an enumeration:
```csharp
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
```

For the above scenario, the second generic parameter of `Color` is used to cast the derived enumeration items to that common base class.

### Custom Enumeration Value Types

It is also possible to enumerate on more complex data types.

Define a data type:
```csharp
public sealed class MyVersionItem
{
	public Action Callback { get; }

	public string DocumentationUrl => $"my-site/versions/{Version}";

	public string Version { get; }

	public MyVersionItem(string version, Action callback) =>
		(Version, Callback) = (version, callback);
}
```

Define an enumeration that has values of that data type
```csharp
public sealed record MyVersions : Enumeration<MyVersionItem>
{
	public static MyVersions V_1_0_0 { get; } = new(
		nameof(V_1_0_0),
		new("1.0.0", () => Console.WriteLine("This is version 1.0.0"))
	);

	public static MyVersions V_1_0_1 { get; } = new(
		nameof(V_1_0_1),
		new("1.0.1", () => Console.WriteLine("This is version 1.0.1"))
	);

	public static MyVersions V_1_0_2 { get; } = new(
		nameof(V_1_0_2),
		new("1.0.2", () => Console.WriteLine("This is version 1.0.2"))
	);

	private MyVersions(string name, MyVersionItem value)
	: base(name, value) { }
}
```

Get a handle on one of the enumeration items.
```csharp
var versionNumber = "1.0.2";
var version =
	Enumeration<MyVersionItem>
		.GetAll<MyVersions>()
		.First((item) => item.Value.Version == versionNumber);
```

Utilize the class members of that complex data type from the enumeration item.
```csharp
version.Value.Callback();
Console.WriteLine(
	"For more details, see the documentation at "
	+ version.Value.DocumentationUrl
);
```

## Links

Sample Project:
https://gitlab.com/reilly-digital/dotnet/enumerations.enumeration/-/tree/main/src/Enumerations.Enumeration.Sample

NuGet:
https://www.nuget.org/packages/ReillyDigital.Enumerations.Enumeration
