using System.ComponentModel;
using System.Runtime.CompilerServices;
using Kirisoup.Diagnostics.TypeUsageRules;

namespace Kirisoup.Lib.Variant;

partial class Result
{
	partial struct Ok<T>
	{
		public Ok<U>.Task and_then<U>(Func<T, ValueTask<Ok<U>>> f) => new(Tand_then(f));
		async ValueTask<Ok<U>> Tand_then<U>(Func<T, ValueTask<Ok<U>>> f) =>
			_opt._isSome ? await f(_opt._some) : Result.Err<U>();
		
		public Task or_else(Func<ValueTask<Ok<T>>> f) => new(Tor_else(f));
		async ValueTask<Ok<T>> Tor_else(Func<ValueTask<Ok<T>>> f) => !_opt._isSome ? await f() : this;
		
		public Result<T, E>.Task or_else<E>(Func<ValueTask<Result<T, E>>> f) => new(Tor_else(f));
		async ValueTask<Result<T, E>> Tor_else<E>(Func<ValueTask<Result<T, E>>> f) => 
			!_opt._isSome ? await f() : Result.Ok<T, E>(_opt._some);

		[NoNew, NoDefault]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public readonly partial struct Task
		{
			internal readonly ValueTask<Ok<T>> _task;
			internal Task(ValueTask<Ok<T>> task) => _task = task;
			public ValueTask<Ok<T>> ToTask() => _task;
			public ValueTaskAwaiter<Ok<T>> GetAwaiter() => _task.GetAwaiter();
		}
	}

	partial struct Err<E>
	{
		public Task and_then(Func<ValueTask<Err<E>>> f) => new(Tand_then(f));
		async ValueTask<Err<E>> Tand_then(Func<ValueTask<Err<E>>> f) => !_opt._isSome ? await f() : this;

		public Result<T, E>.Task and_then<T>(Func<ValueTask<Result<T, E>>> f) => new(Tand_then(f));
		async ValueTask<Result<T, E>> Tand_then<T>(Func<ValueTask<Result<T, E>>> f) =>
			!_opt._isSome ? await f() : Result.Err<T, E>(_opt._some);

		public Err<F>.Task or_else<F>(Func<E, ValueTask<Err<F>>> f) => new(Tor_else(f));
		async ValueTask<Err<F>> Tor_else<F>(Func<E, ValueTask<Err<F>>> f) => 
			_opt._isSome ? await f(_opt._some) : Result.Ok<F>();

		[NoNew, NoDefault]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public readonly partial struct Task
		{
			internal readonly ValueTask<Err<E>> _task;
			internal Task(ValueTask<Err<E>> task) => _task = task;
			public ValueTask<Err<E>> ToTask() => _task;
			public ValueTaskAwaiter<Err<E>> GetAwaiter() => _task.GetAwaiter();
		}
	}
}

public static partial class TaskToMonad {
	public static Result.Ok<T>.Task to_task_monad<T>(this ValueTask<Result.Ok<T>> task) => new(task);
	public static Result.Err<E>.Task to_task_monad<E>(this ValueTask<Result.Err<E>> task) => new(task);
}
