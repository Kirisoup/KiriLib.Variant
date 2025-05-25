using System.Runtime.CompilerServices;
using Kirisoup.Diagnostics.TypeUsageRules;

namespace Kirisoup.Lib.Variant;

partial struct Option<T> {partial struct Task
{
	public async ValueTask<bool> IsSome() => (await _task).IsSome();
	public async ValueTask<bool> IsNone() => (await _task).IsNone();

	public async ValueTask<bool> IsSomeAnd(Func<T, bool> predicate) => (await _task).IsSomeAnd(predicate);
	public async ValueTask<bool> IsNoneOr(Func<T, bool> predicate) => (await _task).IsNoneOr(predicate);

	public async ValueTask<T> Unwrap() => (await _task).Unwrap();
	public async ValueTask<T> Expect(string msg) => (await _task).Expect(msg);

	public async ValueTask<T> SomeOr(T @default) => (await _task).SomeOr(@default);
	public async ValueTask<T> SomeOr(Func<T> @else) => (await _task).SomeOr(@else);

}}
