namespace Kirisoup.Lib.Variant;

partial struct Option<T> 
{
	public Result.Ok<T> ok() => new(this);
	public Result.Err<T> err() => new(this);

	/// <remarks>
	/// <c>.ok(or: _)</c>
	/// </remarks>
	public Result<T, E> ok<E>(E or) => new(_isSome, _some, !_isSome ? or : default!); 
		// new(_var, _some, or) would be technically correct, but might pin a pointer and get in the way of gc

	/// <remarks>
	/// <c>.ok(or_else: _)</c>
	/// </remarks>
	public Result<T, E> ok<E>(Func<E> or_else) => new(_isSome, _some, !_isSome ? or_else() : default!);

	/// <remarks>
	/// <c>.err(or: _)</c>
	/// </remarks>
	public Result<t, T> err<t>(t or) => new(!_isSome, !_isSome ? or : default!, _some); 

	/// <remarks>
	/// <c>.err(or_else: _)</c>
	/// </remarks>
	public Result<t, T> err<t>(Func<t> or_else) => new(!_isSome, !_isSome ? or_else() : default!, _some);

	public Option<U> map<U>(Func<T, U> f) => new(_isSome, _isSome ? f(_some) : default!);
	public Option<T> inspect(Action<T> f) { if (_isSome) f(_some); return this; }

	/// <remarks>
	/// <c>.map(or: _, f)</c>
	/// </remarks>
	public U map<U>(U or, Func<T, U> f) => _isSome ? f(_some) : or;

	/// <remarks>
	/// <c>.map(or_else: _, f)</c>
	/// </remarks>
	public U map<U>(Func<U> or_else, Func<T, U> f) => _isSome ? f(_some) : or_else();

	public Option<U> and<U>(Option<U> other) => _isSome ? other : Option.None<U>();
	public Option<U> and_then<U>(Func<T, Option<U>> f) => _isSome ? f(_some) : Option.None<U>();

	public Option<T> or(Option<T> other) => !_isSome ? other : this;
	public Option<T> or_else(Func<Option<T>> f) => !_isSome ? f() : this;

	public Option<U> cast<U>() {
		if (!_isSome) return Option.None<U>();
		if (_some is U u) return Option.Some(u);
		try {
			return Option.Some((U)((object?)_some)!);
		}
		catch (InvalidCastException) {
			return Option.None<U>();
		}
	}
}