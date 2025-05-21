namespace Kirisoup.Lib.Variant;

partial struct Option<T> 
{
	public T unwrap() => IsSome ? _some : throw new UnwrapException($"None<{typeof(T)}>");
	public T expect(string msg) => IsSome ? _some : throw new ExpectException(msg);

	/// <remarks>
	/// <c>.some(or: _)</c>
	/// </remarks>
	public T some(T or) => IsSome ? _some : or;

	/// <remarks>
	/// <c>.some(or_else: _)</c>
	/// </remarks>
	public T some(Func<T> or_else) => IsSome ? _some : or_else();

	public Result.Ok<T> ok() => new(this);
	public Result.Err<T> err() => new(this);

	/// <remarks>
	/// <c>.ok(or: _)</c>
	/// </remarks>
	public Result<T, E> ok<E>(E or) => new(_var, _some, IsNone ? or : default!); 
		// new(_var, _some, or) would be technically correct, but might pin a pointer and get in the way of gc

	/// <remarks>
	/// <c>.ok(or_else: _)</c>
	/// </remarks>
	public Result<T, E> ok<E>(Func<E> or_else) => new(_var, _some, IsNone ? or_else() : default!);

	/// <remarks>
	/// <c>.err(or: _)</c>
	/// </remarks>
	public Result<t, T> err<t>(t or) => new(_var.Invert(), IsNone ? or : default!, _some); 

	/// <remarks>
	/// <c>.err(or_else: _)</c>
	/// </remarks>
	public Result<t, T> err<t>(Func<t> or_else) => new(_var.Invert(), IsNone ? or_else() : default!, _some);

	public Option<U> map<U>(Func<T, U> f) => new(_var, IsSome ? f(_some) : default!);
	public Option<T> inspect(Action<T> f) { if (IsSome) f(_some); return this; }

	/// <remarks>
	/// <c>.map(or: _, f)</c>
	/// </remarks>
	public U map<U>(U or, Func<T, U> f) => IsSome ? f(_some) : or;

	/// <remarks>
	/// <c>.map(or_else: _, f)</c>
	/// </remarks>
	public U map<U>(Func<U> or_else, Func<T, U> f) => IsSome ? f(_some) : or_else();

	public Option<U> and<U>(Option<U> other) => IsSome ? other : Option.None<U>();
	public Option<U> and_then<U>(Func<T, Option<U>> f) => IsSome ? f(_some) : Option.None<U>();

	public Option<T> or(Option<T> other) => IsNone ? other : this;
	public Option<T> or_else(Func<Option<T>> f) => IsNone ? f() : this;
}