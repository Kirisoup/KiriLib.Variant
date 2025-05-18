namespace KiriLib.Variant;

partial struct Result<T, E> 
{
	public T unwrap() => IsOk ? _ok : throw new UnwrapException($"Err({_err.ToStringNullable()})");
	public E unwrap_err() => IsErr ? _err : throw new UnwrapException($"Ok({_ok.ToStringNullable()})");

	public T expect(string msg) => IsOk ? _ok : throw new ExpectException(msg);
	public E expect_err(string msg) => IsErr ? _err : throw new ExpectException(msg);

	public Option<T> ok() => new(_var, _ok);
	public Option<E> err() => new(_var.Invert(), _err);

	/// <remarks>
	/// <c>.ok(or: _)</c>
	/// </remarks>
	public T ok(T or) => IsOk ? _ok : or;

	/// <remarks>
	/// <c>.ok(or_else: _)</c>
	/// </remarks>
	public T ok(Func<T> or_else) => IsOk ? _ok : or_else();
	
	public Result<U, E> map<U>(Func<T, U> f) => new(_var, IsOk ? f(_ok) : default!, _err);
	public Result<T, F> map_err<F>(Func<E, F> f) => new(_var, _ok, IsErr ? f(_err) : default!);

	public Result<T, E> inspect(Action<T> f) { if (IsOk) f(_ok); return this; }
	public Result<T, E> inspect_err(Action<E> f) { if (IsErr) f(_err); return this; }

	/// <remarks>
	/// <c>.map(or: _, f)</c>
	/// </remarks>
	public U map<U>(U or, Func<T, U> f) => IsOk ? f(_ok) : or;

	/// <remarks>
	/// <c>.map(or_else: _, f)</c>
	/// </remarks>
	public U map<U>(Func<U> or_else, Func<T, U> f) => IsOk ? f(_ok) : or_else();
	
	public Result<U, E> and<U>(Result<U, E> other) => IsOk ? other : Result.Err<U, E>(_err);
	public Result<U, E> and_then<U>(Func<T, Result<U, E>> f) => IsOk ? f(_ok) : Result.Err<U, E>(_err);

	public Result.Err<E> and(Result.Err<E> other) => IsOk ? other : Result.Err(_err);
	public Result.Err<E> and_then(Func<T, Result.Err<E>> f) => IsOk ? f(_ok) : Result.Err(_err);

	public Result<T, F> or<F>(Result<T, F> other) => IsErr ? other : Result.Ok<T, F>(_ok);
	public Result<T, F> or_else<F>(Func<E, Result<T, F>> f) => IsErr ? f(_err) : Result.Ok<T, F>(_ok);

	public Result.Ok<T> or(Result.Ok<T> other) => IsErr ? other : Result.Ok(_ok);
	public Result.Ok<T> or_else(Func<E, Result.Ok<T>> f) => IsErr ? f(_err) : Result.Ok(_ok);
}

internal static class ToStringNullableUtil 
{
	public static string ToStringNullable<T>(this T? self) => self?.ToString() ?? $"null<{typeof(T)}>";
}