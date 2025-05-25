namespace Kirisoup.Lib.Variant;

partial class Result
{
	partial struct Ok<T>
	{
		public Option<T> to_option() => _opt;

		public Ok<U> map<U>(Func<T, U> f) => new(_opt.map(f));
		public Result<T, F> map_err<F>(Func<F> f) => _opt.ok(or_else: f);

		public Ok<T> inspect(Action<T> f) { if (_opt._isSome) f(_opt._some); return this; }

		/// <inheritdoc cref="Result{T, E}.map{U}(U, Func{T, U})" />
		public U map<U>(U or, Func<T, U> f) => _opt.map(or, f);

		/// <inheritdoc cref="Result{T, E}.map{U}(Func{U}, Func{T, U})" />
		public U map<U>(Func<U> or_else, Func<T, U> f) => _opt.map(or_else, f);

		public Ok<U> and<U>(Ok<U> other) => new(_opt.and(other._opt));
		public Ok<U> and_then<U>(Func<T, Ok<U>> f) => _opt._isSome ? f(_opt._some) : Result.Err<U>();

		public Ok<T> or(Ok<T> other) => new(_opt.or(other._opt));
		public Ok<T> or_else(Func<Ok<T>> f) => !_opt._isSome ? f() : this;

		public Result<T, E> or<E>(Result<T, E> other) => !_opt._isSome ? other : Result.Ok<T, E>(_opt._some);
		public Result<T, E> or_else<E>(Func<Result<T, E>> f) => !_opt._isSome ? f() : Result.Ok<T, E>(_opt._some);	
	}

	partial struct Err<E>
	{
		public Option<E> to_option() => _opt;

		public Result<T, E> map<T>(Func<T> f) => _opt.err(or_else: f);
		public Err<F> map_err<F>(Func<E, F> f) => new(_opt.map(f));

		public Err<E> inspect_err(Action<E> f) { if (_opt._isSome) f(_opt._some); return this; }
		
		public Err<E> and(Err<E> other) => new(_opt.or(other._opt));
		public Err<E> and_then(Func<Err<E>> f) => !_opt._isSome ? f() : this;

		public Result<T, E> and<T>(Result<T, E> other) => !_opt._isSome ? other : Result.Err<T, E>(_opt._some);
		public Result<T, E> and_then<T>(Func<Result<T, E>> f) => !_opt._isSome ? f() : Result.Err<T, E>(_opt._some);

		public Err<F> or<F>(Err<F> other) => new(_opt.and(other._opt));
		public Err<F> or_else<F>(Func<E, Err<F>> f) => _opt._isSome ? f(_opt._some) : Result.Ok<F>();
	}
}