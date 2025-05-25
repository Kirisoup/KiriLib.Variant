namespace Kirisoup.Lib.Variant;

partial class Result
{
	partial struct Ok<T> {partial struct Task
	{
		public async ValueTask<bool> IsOk() => (await _task).IsOk();
		public async ValueTask<bool> IsErr() => (await _task).IsErr();

		public async ValueTask<bool> IsOkAnd(Func<T, bool> predicate) => (await _task).IsOkAnd(predicate);
		public async ValueTask<bool> IsErrOr(Func<T, bool> predicate) => (await _task).IsErrOr(predicate);

		public async ValueTask<T> Unwrap() => (await _task).Unwrap();

		public async ValueTask<T> Expect(string msg) => (await _task).Expect(msg);
		public async ValueTask ExpectErr(string msg) => (await _task).ExpectErr(msg);

		public async ValueTask<T> OkOr(T @default) => (await _task).OkOr(@default);
		public async ValueTask<T> OkOr(Func<T> @else) => (await _task).OkOr(@else);
	}}

	partial struct Err<E> {partial struct Task
	{
		public async ValueTask<bool> IsOk() => (await _task).IsOk();
		public async ValueTask<bool> IsErr() => (await _task).IsErr();

		public async ValueTask<bool> IsOkOr(Func<E, bool> predicate) => (await _task).IsOkOr(predicate);
		public async ValueTask<bool> IsErrAnd(Func<E, bool> predicate) => (await _task).IsErrAnd(predicate);

		public async ValueTask<E> UnwrapErr() => (await _task).UnwrapErr();

		public async ValueTask Expect(string msg) => (await _task).Expect(msg);
		public async ValueTask<E> ExpectErr(string msg) => (await _task).ExpectErr(msg);
	}}
}