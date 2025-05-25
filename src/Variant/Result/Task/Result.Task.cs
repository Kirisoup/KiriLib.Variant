using System.Runtime.CompilerServices;
using Kirisoup.Diagnostics.TypeUsageRules;

namespace Kirisoup.Lib.Variant;

partial struct Result<T, E>
{
	public Result<U, E>.Task and_then<U>(Func<T, ValueTask<Result<U, E>>> f) => new(Tand_then(f));
	async ValueTask<Result<U, E>> Tand_then<U>(Func<T, ValueTask<Result<U, E>>> f) => 
		_isOk ? await f(_ok) : Result.Err<U, E>(_err);

	public Result.Err<E>.Task and_then(Func<T, ValueTask<Result.Err<E>>> f) => new(Tand_then(f));
	async ValueTask<Result.Err<E>> Tand_then(Func<T, ValueTask<Result.Err<E>>> f) => 
		_isOk ? await f(_ok) : Result.Err(_err);

	public Result<T, F>.Task or_else<F>(Func<E, ValueTask<Result<T, F>>> f) => new(Tor_else(f));
	async ValueTask<Result<T, F>> Tor_else<F>(Func<E, ValueTask<Result<T, F>>> f) => 
		!_isOk ? await f(_err) : Result.Ok<T, F>(_ok);

	public Result.Ok<T>.Task or_else(Func<E, ValueTask<Result.Ok<T>>> f) => new(Tor_else(f));
	async ValueTask<Result.Ok<T>> Tor_else(Func<E, ValueTask<Result.Ok<T>>> f) => 
		!_isOk ? await f(_err) : Result.Ok(_ok);

	[NoNew, NoDefault]
	public readonly partial struct Task
	{
		internal readonly ValueTask<Result<T, E>> _task;
		internal Task(ValueTask<Result<T, E>> task) => _task = task;
		public ValueTaskAwaiter<Result<T, E>> GetAwaiter() => _task.GetAwaiter();
	}
}

public static partial class TaskToMonad {
extension <T, E> (ValueTask<Result<T, E>> task)
{
	public Result<T, E>.Task to_task_monad() => new(task);
}
}
