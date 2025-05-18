#pragma warning disable CS0618
namespace KiriLib.Variant;

/// <summary>
/// Representing either an ok result of <see cref="T" /> or an error result of <see cref="E" />
/// </summary>
public readonly partial struct Result<T, E>
{
	readonly Variant _var;
	readonly T _ok;
	readonly E _err;

	internal Result(Variant variant, T ok, E err) {
		_var = variant;
		_ok = ok;
		_err = err;
	}

	public static implicit operator Result<T, E>(__intermediates.Ok<T> ok) => Result.Ok<T, E>(ok._value);
	public static implicit operator Result<T, E>(__intermediates.Err<E> err) => Result.Err<T, E>(err._value);

	[Obsolete("", error: true)] public Result() => throw new NotSupportedException();
}

public static partial class Result;

public static partial class ResultCtorSuger {
extension (Result) {
	public static Result<T, E> Ok<T, E>(T value) => new(Variant.Expected, value, default!);
	public static __intermediates.Ok<T> Ok<T>(T value) => new(value);
	public static Result<T, E> Err<T, E>(E value) => new(Variant.Unexpected, default!, value);
	public static __intermediates.Err<E> Err<E>(E value) => new(value);
}
}