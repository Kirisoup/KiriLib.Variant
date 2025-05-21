#pragma warning disable CS0618
namespace Kirisoup.Lib.Variant;

/// <summary>
/// Representing either a value of <see cref="T" /> or nothing. 
/// </summary>
public readonly partial struct Option<T> 
{
	readonly Variant _var;
	readonly T _some;

	internal Option(Variant variant, T some) {
		_var = variant;
		_some = some;
	}	

	public static implicit operator Option<T>(T value) => Option.Some(value);
	public static implicit operator Option<T>(__intermediates.None _) => Option.None<T>();

	[Obsolete("", error: true)] public Option() => throw new NotSupportedException();
}

public static class Option {
	public static Option<T> Some<T>(T value) => new(Variant.Expected, value);
	public static Option<T> None<T>() => new(Variant.Unexpected, default!);
	public static __intermediates.None None() => new();
}
