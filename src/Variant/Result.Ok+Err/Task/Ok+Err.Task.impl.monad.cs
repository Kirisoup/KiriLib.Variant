namespace Kirisoup.Lib.Variant;

partial class Result
{
	partial struct Ok<T> {partial struct Task
	{
		public Option<T>.Task to_option() => new(Tto_option());
		async ValueTask<Option<T>> Tto_option() => (await _task).to_option();


		public Ok<U>.Task map<U>(Func<T, U> f) => new(Tmap(f));
		async ValueTask<Ok<U>> Tmap<U>(Func<T, U> f) => (await _task).map(f);

		public Result<T, F>.Task map_err<F>(Func<F> f) => new(Tmap_err(f));
		async ValueTask<Result<T, F>> Tmap_err<F>(Func<F> f) => (await _task).map_err(f);


		public Task inspect(Action<T> f) => new(Tinspect(f));
		async ValueTask<Ok<T>> Tinspect(Action<T> f) => (await _task).inspect(f);


		public async ValueTask<U> map<U>(U or, Func<T, U> f) => (await _task).map(or, f);
		public async ValueTask<U> map<U>(Func<U> or_else, Func<T, U> f) => (await _task).map(or_else, f);


		public Ok<U>.Task and<U>(Ok<U> other) => new(Tand(other));
		async ValueTask<Ok<U>> Tand<U>(Ok<U> other) => (await _task).and(other);

		public Ok<U>.Task and_then<U>(Func<T, Ok<U>> f) => new(Tand_then(f));
		async ValueTask<Ok<U>> Tand_then<U>(Func<T, Ok<U>> f) => (await _task).and_then(f);

		public Ok<U>.Task and_then<U>(Func<T, ValueTask<Ok<U>>> f) => new(Tand_then(f));
		async ValueTask<Ok<U>> Tand_then<U>(Func<T, ValueTask<Ok<U>>> f) => await (await _task).Tand_then(f);


		public Task or(Ok<T> other) => new(Tor(other));
		async ValueTask<Ok<T>> Tor(Ok<T> other) => (await _task).or(other);

		public Task or_else(Func<Ok<T>> f) => new(Tor_else(f));
		async ValueTask<Ok<T>> Tor_else(Func<Ok<T>> f) => (await _task).or_else(f);

		public Task or_else(Func<ValueTask<Ok<T>>> f) => new(Tor_else(f));
		async ValueTask<Ok<T>> Tor_else(Func<ValueTask<Ok<T>>> f) => await (await _task).Tor_else(f);


		public Result<T, E>.Task or<E>(Result<T, E> other) => new(Tor_result(other));
		async ValueTask<Result<T, E>> Tor_result<E>(Result<T, E> other) => (await _task).or(other);

		public Result<T, E>.Task or_else<E>(Func<Result<T, E>> f) => new(Tor_else_result(f));
		async ValueTask<Result<T, E>> Tor_else_result<E>(Func<Result<T, E>> f) => (await _task).or_else(f);

		public Result<T, E>.Task or_else<E>(Func<ValueTask<Result<T, E>>> f) => new(Tor_else_result(f));
		async ValueTask<Result<T, E>> Tor_else_result<E>(Func<ValueTask<Result<T, E>>> f) => 
			await (await _task).Tor_else(f);
	}}

	partial struct Err<E> {partial struct Task
	{
		public Option<E>.Task to_option() => new(Tto_option());
		async ValueTask<Option<E>> Tto_option() => (await _task).to_option();


		public Result<T, E>.Task map<T>(Func<T> f) => new(Tmap(f));
		async ValueTask<Result<T, E>> Tmap<T>(Func<T> f) => (await _task).map(f);

		public Err<F>.Task map_err<F>(Func<E, F> f) => new(Tmap_err(f));
		async ValueTask<Err<F>> Tmap_err<F>(Func<E, F> f) => (await _task).map_err(f);


		public Task inspect_err(Action<E> f) => new(Tinspect_err(f));
		async ValueTask<Err<E>> Tinspect_err(Action<E> f) => (await _task).inspect_err(f);


		public Task and(Err<E> other) => new(Tand(other));
		async ValueTask<Err<E>> Tand(Err<E> other) => (await _task).and(other);

		public Task and_then(Func<Err<E>> f) => new(Tand_then(f));
		async ValueTask<Err<E>> Tand_then(Func<Err<E>> f) => (await _task).and_then(f);

		public Task and_then(Func<ValueTask<Err<E>>> f) => new(Tand_then(f));
		async ValueTask<Err<E>> Tand_then(Func<ValueTask<Err<E>>> f) => await (await _task).Tand_then(f);


		public Result<T, E>.Task and<T>(Result<T, E> other) => new(Tand_result(other));
		async ValueTask<Result<T, E>> Tand_result<T>(Result<T, E> other) => (await _task).and(other);

		public Result<T, E>.Task and_then<T>(Func<Result<T, E>> f) => new(Tand_then_result(f));
		async ValueTask<Result<T, E>> Tand_then_result<T>(Func<Result<T, E>> f) => (await _task).and_then(f);

		public Result<T, E>.Task and_then<T>(Func<ValueTask<Result<T, E>>> f) => new(Tand_then_result(f));
		async ValueTask<Result<T, E>> Tand_then_result<T>(Func<ValueTask<Result<T, E>>> f) => 
			await (await _task).Tand_then(f);


		public Err<F>.Task or<F>(Err<F> other) => new(Tor(other));
		async ValueTask<Err<F>> Tor<F>(Err<F> other) => (await _task).or(other);

		public Err<F>.Task or_else<F>(Func<E, Err<F>> f) => new(Tor_else(f));
		async ValueTask<Err<F>> Tor_else<F>(Func<E, Err<F>> f) => (await _task).or_else(f);

		public Err<F>.Task or_else<F>(Func<E, ValueTask<Err<F>>> f) => new(Tor_else(f));
		async ValueTask<Err<F>> Tor_else<F>(Func<E, ValueTask<Err<F>>> f) => await (await _task).Tor_else(f);
	}}
}