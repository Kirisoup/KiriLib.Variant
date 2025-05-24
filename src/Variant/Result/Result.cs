#pragma warning disable CS0618
using Kirisoup.Diagnostics.TypeUsageRules;

namespace Kirisoup.Lib.Variant;

/// <summary>
/// Representing either an ok result of <see cref="T" /> or an error result of <see cref="E" />
/// </summary>
[NoDefault, NoNew]
public readonly partial struct Result<T, E>
{
	internal readonly bool _isOk;
	internal readonly T _ok;
	internal readonly E _err;

	internal Result(bool isOk, T ok, E err) {
		_isOk = isOk;
		_ok = ok;
		_err = err;
	}

	public static implicit operator Result<T, E>(Intermediates.Ok<T> ok) => Result.Ok<T, E>(ok._value);
	public static implicit operator Result<T, E>(Intermediates.Err<E> err) => Result.Err<T, E>(err._value);
	public static implicit operator Result<T, E>(T value) => Result.Ok<T, E>(value);
}

public static partial class Result;

public static partial class ResultCtorSugar {
extension (Result) {
	public static Result<T, E> Ok<T, E>(T value) => new(true, value, default!);
	public static Intermediates.Ok<T> Ok<T>(T value) => new(value);
	public static Result<T, E> Err<T, E>(E value) => new(false, default!, value);
	public static Intermediates.Err<E> Err<E>(E value) => new(value);
}
}