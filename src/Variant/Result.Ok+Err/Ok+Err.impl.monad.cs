namespace KiriLib.Variant;

partial class Result
{
	partial struct Ok<T>
	{
		public T unwrap() => _opt.unwrap();

		public T expect(string msg) => _opt.expect(msg);
		public void expect_err(string msg) { if (IsOk) throw new ExpectException(msg); }

		public Option<T> ok() => _opt;

		/// <inheritdoc cref="Result{T, E}.ok(T)" />
		public T ok(T or) => _opt.some(or);

		/// <inheritdoc cref="Result{T, E}.ok(Func{T})" />
		public T ok(Func<T> or_else) => _opt.some(or_else);
	}

	partial struct Err<E>
	{
		public E unwrap_err() => _opt.unwrap();

		public void expect(string msg) { if (IsErr) throw new ExpectException(msg); }
		public E expect_err(string msg) => _opt.expect(msg);

		public Option<E> err() => _opt;

	}

	partial struct Ok<T>
	{
		public Ok<U> map<U>(Func<T, U> f) => new(_opt.map(f));
		public Result<T, F> map_err<F>(Func<F> f) => _opt.ok(or_else: f); // R<T, ()> -> (() -> F) -> R<T, F>

		public Ok<T> inspect(Action<T> f) { _opt.inspect(f); return this; }

		/// <inheritdoc cref="Result{T, E}.map{U}(U, Func{T, U})" />
		public U map<U>(U or, Func<T, U> f) => _opt.map(or, f);

		/// <inheritdoc cref="Result{T, E}.map{U}(Func{U}, Func{T, U})" />
		public U map<U>(Func<U> or_else, Func<T, U> f) => _opt.map(or_else, f);

		public Ok<U> and<U>(Ok<U> other) => new(_opt.and(other._opt));
		public Ok<U> and_then<U>(Func<T, Ok<U>> f) => new(_opt.and_then(x => f(x)._opt));

		public Result<T, E> or<E>(Result<T, E> other) => IsErr ? other : Result.Ok<T, E>(_opt.unwrap());
		public Result<T, E> or_else<E>(Func<Result<T, E>> f) => IsErr ? f() : Result.Ok<T, E>(_opt.unwrap());	
	}

	partial struct Err<E>
	{
		public Result<T, E> map<T>(Func<T> f) => _opt.err(or_else: f);
		public Err<F> map_err<F>(Func<E, F> f) => new(_opt.map(f));

		public Err<E> inspect_err(Action<E> f) { _opt.inspect(f); return this; }
		
		public Result<T, E> and<T>(Result<T, E> other) => IsOk ? other : Result.Err<T, E>(_opt.unwrap());
		public Result<T, E> and_then<T>(Func<Result<T, E>> f) => IsOk ? f() : Result.Err<T, E>(_opt.unwrap());

		public Err<F> or<F>(Err<F> other) => new(_opt.and(other._opt));
		public Err<F> or_else<F>(Func<E, Err<F>> f) => new(_opt.and_then(x => f(x)._opt));
	}
}