namespace Kirisoup.Lib.Variant;

partial class Result
{
	partial struct Ok<T>
	{
		public bool IsOk() => _opt.IsSome();
		public bool IsErr() => _opt.IsNone();

		public bool IsOkAnd(Func<T, bool> predicate) => _opt.IsSomeAnd(predicate);
		public bool IsErrOr(Func<T, bool> predicate) => _opt.IsNoneOr(predicate);

		/// <inheritdoc cref="Result{T, E}.IsOk(out T)" />
		public bool IsOk(out T ok) => _opt.IsSome(out ok);

		public T Unwrap() => _opt.Unwrap();

		public T Expect(string msg) => _opt.Expect(msg);
		public void ExpectErr(string msg) { if (IsOk()) throw new ExpectException(msg); }

		public T OkOr(T @default) => _opt.SomeOr(@default);
		public T OkOr(Func<T> @else) => _opt.SomeOr(@else);
	}

	partial struct Err<E>
	{
		public bool IsOk() => _opt.IsNone();
		public bool IsErr() => _opt.IsSome();

		public bool IsOkOr(Func<E, bool> predicate) => _opt.IsNoneOr(predicate);
		public bool IsErrAnd(Func<E, bool> predicate) => _opt.IsSomeAnd(predicate);

		/// <inheritdoc cref="Result{T, E}.IsErr(out E)" />
		public bool IsErr(out E err) => _opt.IsSome(out err);

		public E UnwrapErr() => _opt.Unwrap();

		public void Expect(string msg) { if (IsErr()) throw new ExpectException(msg); }
		public E ExpectErr(string msg) => _opt.Expect(msg);
	}
}