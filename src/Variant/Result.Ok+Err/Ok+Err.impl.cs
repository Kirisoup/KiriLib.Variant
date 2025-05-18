namespace KiriLib.Variant;

partial class Result
{
	partial struct Ok<T>
	{
		public bool IsOk => _opt.IsSome;
		public bool IsErr => _opt.IsNone;

		public bool IsOkAnd(Func<T, bool> predicate) => _opt.IsSomeAnd(predicate);
		public bool IsErrOr(Func<T, bool> predicate) => _opt.IsNoneOr(predicate);

		/// <inheritdoc cref="Result{T, E}.IsOkThen(out T)" />
		public bool IsOkThen(out T ok) => _opt.IsSomeThen(out ok);
	}

	partial struct Err<E>
	{
		public bool IsOk => _opt.IsNone;
		public bool IsErr => _opt.IsSome;

		public bool IsOkOr(Func<E, bool> predicate) => _opt.IsNoneOr(predicate);
		public bool IsErrAnd(Func<E, bool> predicate) => _opt.IsSomeAnd(predicate);

		/// <inheritdoc cref="Result{T, E}.IsErrThen(out E)" />
		public bool IsErrThen(out E err) => _opt.IsSomeThen(out err);
	}
}