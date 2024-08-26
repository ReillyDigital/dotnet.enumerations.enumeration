namespace ReillyDigital.Enumerations.Sample.Enums.Versions;

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

public sealed class MyVersionItem
{
	public Action Callback { get; }

	public string DocumentationUrl => $"my-site/versions/{Version}";

	public string Version { get; }

	public MyVersionItem(string version, Action callback) =>
		(Version, Callback) = (version, callback);
}
