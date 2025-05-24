#pragma warning disable CS0618

using Kirisoup.Diagnostics.TypeUsageRules;

namespace Kirisoup.Lib.Variant;

/// <summary>
/// Representing either a value of <see cref="T" /> or nothing. 
/// </summary>
[NoDefault, NoNew]
public readonly partial struct Option<T>
{
	internal readonly bool _isSome;
	internal readonly T _some;

	internal Option(bool isSome, T some)
	{
		_isSome = isSome;
		_some = some;
	}

	public static implicit operator Option<T>(T value) => Option.Some(value);
	public static implicit operator Option<T>(Intermediates.None _) => Option.None<T>();
}

public static class Option
{
	public static Option<T> Some<T>(T value) => new(true, value);
	public static Option<T> None<T>() => new(false, default!);
	public static Intermediates.None None() => new();
}
