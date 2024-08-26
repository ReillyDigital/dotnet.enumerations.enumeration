namespace ReillyDigital.Enumerations;

using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Represents an enumeration of <see cref="T" />.
/// </summary>
public abstract record Enumeration : Enumeration<string>
{
	/// <summary>
	/// Implicit cast operator from a <see cref="string" />.
	/// </summary>
	public static implicit operator string(Enumeration value) => value.Name;

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="name">The name of the enumeration item.</param>
	protected Enumeration(string name) : base(name, name) { }
}

/// <summary>
/// Represents an enumeration of <see cref="T" />.
/// </summary>
public abstract record Enumeration<T>
{
	/// <summary>
	/// Implicit cast operator from a <see cref="T" />.
	/// </summary>
	public static implicit operator T(Enumeration<T> value) => value.Value;

	/// <summary>
	/// Gets all static property values on the enumeration type <see cref="TEnumeration" /> where the property type is
	/// of that same enumeration type.
	/// </summary>
	/// <returns>A collection of the defined enumeration items.</returns>
	public static IEnumerable<TEnumeration> GetAll<TEnumeration>() where TEnumeration : Enumeration<T>
	=> GetAll<TEnumeration, TEnumeration>();

	/// <summary>
	/// Gets all static property values on the enumeration type <see cref="TEnumeration" /> where the property type is
	/// of the item type <see cref="TItem" />. The enumeration type <see cref="TEnumeration" /> must also be assignable
	/// to the item type <see cref="TItem" />.
	/// </summary>
	/// <returns>A collection of the defined enumeration items.</returns>
	public static IEnumerable<TItem> GetAll<TEnumeration, TItem>()
	where TEnumeration : TItem
	where TItem : Enumeration<T>
	=> typeof(TEnumeration)
		.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
		.Where((fieldInfo) => fieldInfo.PropertyType.IsAssignableTo(typeof(TItem)))
		.Select((fieldInfo) => fieldInfo.GetValue(null))
		.Cast<TItem>();

	/// <summary>
	/// Parse an enumeration item by its name, where the item type is of <see cref="TEnumeration" />.
	/// </summary>
	/// <param name="name">The name of the enumeration item to parse.</param>
	/// <returns>The enumeration item of the provided name if it exists.</returns>
	public static TEnumeration Parse<TEnumeration>(string name) where TEnumeration : Enumeration<T>
	=> Parse<TEnumeration, TEnumeration>(name);

	/// <summary>
	/// Parse an enumeration item by its name, where the item type is of <see cref="TItem" />, and
	/// <see cref="TEnumeration" /> is assignable to <see cref="TItem" />.
	/// </summary>
	/// <param name="name">The name of the enumeration item to parse.</param>
	/// <returns>The enumeration item of the provided name if it exists.</returns>
	/// <exception cref="KeyNotFoundException" />
	public static TItem Parse<TEnumeration, TItem>(string name)
	where TEnumeration : TItem
	where TItem : Enumeration<T>
	{
		_ = TryParse<TEnumeration, TItem>(name, out var returnValue);
		return returnValue ?? throw new KeyNotFoundException("Enumeration could not be parsed.");
	}

	/// <summary>
	/// Try to parse an enumeration item by its name, where the item type is of <see cref="TEnumeration" />.
	/// </summary>
	/// <param name="name">The name of the enumeration item to parse.</param>
	/// <param name="name">Out reference for the parsed enumeration item.</param>
	/// <returns>A Boolean to indicate whether the item was parsed successfully.</returns>
	public static bool TryParse<TEnumeration>(string name, out TEnumeration value)
	where TEnumeration : Enumeration<T>
	=> TryParse<TEnumeration, TEnumeration>(name, out value);

	/// <summary>
	/// Try to parse an enumeration item by its name, where the item type is of <see cref="TItem" />, and
	/// <see cref="TEnumeration" /> is assignable to <see cref="TItem" />.
	/// </summary>
	/// <param name="name">The name of the enumeration item to parse.</param>
	/// <param name="name">Out reference for the parsed enumeration item.</param>
	/// <returns>A Boolean to indicate whether the item was parsed successfully.</returns>
	public static bool TryParse<TEnumeration, TItem>(string name, out TItem value)
		where TEnumeration : TItem
		where TItem : Enumeration<T>
	{
		value = default!;
		var maybe =
			typeof(TEnumeration)
				.GetProperty(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)?
				.GetValue(null);
		if (maybe is null)
		{
			return false;
		}
		value = (TItem)maybe;
		return true;
	}

	/// <summary>
	/// Switch upon an enumeration item, providing enumeration items to try to match against, paired with a value to
	/// return when matched.
	/// </summary>
	/// <param name="value">The enumeration item to switch upon.</param>
	/// <param name="checks">
	/// The pair of an enumeration item and result to check that item against the provided <see cref="value" />, and
	/// return the result if matched.
	/// </param>
	/// <returns>The return value for the first pair that is matched.</returns>
	public static TReturn? Switch<TEnumeration, TReturn>(
		TEnumeration value, params (TEnumeration check, TReturn result)[] checks
	)
	where TEnumeration : Enumeration<T>
	where TReturn : class
	{
		(TEnumeration check, TReturn result)? check = checks.FirstOrDefault((each) => each.check == value);
		return check?.result ?? null;
	}

	/// <summary>
	/// The name of the enumeration item.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// The value of the enumeration item.
	/// </summary>
	public T Value { get; }

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="name">The name of the enumeration item.</param>
	/// <param name="value">The value of the enumeration item.</param>
	protected Enumeration(string name, T value) => (Name, Value) = (name, value);
}
