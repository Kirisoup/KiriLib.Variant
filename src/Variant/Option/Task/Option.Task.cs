using System.ComponentModel;
using System.Runtime.CompilerServices;
using Kirisoup.Diagnostics.TypeUsageRules;

namespace Kirisoup.Lib.Variant;

partial struct Option<T>
{
	public Option<U>.Task and_then<U>(Func<T, ValueTask<Option<U>>> f) => new(Tand_then(f));
	async ValueTask<Option<U>> Tand_then<U>(Func<T, ValueTask<Option<U>>> f) => 
		_isSome ? await f(_some) : Option.None<U>();

	public Task or_else(Func<ValueTask<Option<T>>> f) => new(Tor_else(f));
	async ValueTask<Option<T>> Tor_else(Func<ValueTask<Option<T>>> f) => 
		!_isSome ? await f() : this;

	[NoNew, NoDefault]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public readonly partial struct Task
	{
		internal readonly ValueTask<Option<T>> _task;
		internal Task(ValueTask<Option<T>> task) => _task = task;
		public ValueTask<Option<T>> ToTask() => _task;
		public ValueTaskAwaiter<Option<T>> GetAwaiter() => _task.GetAwaiter();
	}
}

public static partial class TaskToMonad {
	public static Option<T>.Task to_task_monad<T>(this ValueTask<Option<T>> task) => new(task);
}
