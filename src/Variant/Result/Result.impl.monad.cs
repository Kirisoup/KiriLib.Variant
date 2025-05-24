namespace Kirisoup.Lib.Variant;

partial struct Result<T, E> 
{
	public Result.Ok<T> ok() => new(new(_isOk, _ok));
	public Result.Err<E> err() => new(new(!_isOk, _err));

	public Result<U, E> map<U>(Func<T, U> f) => new(_isOk, _isOk ? f(_ok) : default!, _err);
	public Result<T, F> map_err<F>(Func<E, F> f) => new(_isOk, _ok, IsErr() ? f(_err) : default!);

	public Result<T, E> inspect(Action<T> f) { if (_isOk) f(_ok); return this; }
	public Result<T, E> inspect_err(Action<E> f) { if (!_isOk) f(_err); return this; }

	/// <remarks>
	/// <c>.map(or: _, f)</c>
	/// </remarks>
	public U map<U>(U or, Func<T, U> f) => _isOk ? f(_ok) : or;

	/// <remarks>
	/// <c>.map(or_else: _, f)</c>
	/// </remarks>
	public U map<U>(Func<U> or_else, Func<T, U> f) => _isOk ? f(_ok) : or_else();
	
	public Result<U, E> and<U>(Result<U, E> other) => _isOk ? other : Result.Err<U, E>(_err);
	public Result<U, E> and_then<U>(Func<T, Result<U, E>> f) => _isOk ? f(_ok) : Result.Err<U, E>(_err);

	public Result.Err<E> and(Result.Err<E> other) => _isOk ? other : Result.Err(_err);
	public Result.Err<E> and_then(Func<T, Result.Err<E>> f) => _isOk ? f(_ok) : Result.Err(_err);

	public Result<T, F> or<F>(Result<T, F> other) => !_isOk ? other : Result.Ok<T, F>(_ok);
	public Result<T, F> or_else<F>(Func<E, Result<T, F>> f) => !_isOk ? f(_err) : Result.Ok<T, F>(_ok);

	public Result.Ok<T> or(Result.Ok<T> other) => !_isOk ? other : Result.Ok(_ok);
	public Result.Ok<T> or_else(Func<E, Result.Ok<T>> f) => !_isOk ? f(_err) : Result.Ok(_ok);
}
