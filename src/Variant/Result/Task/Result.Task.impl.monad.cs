namespace Kirisoup.Lib.Variant;

partial struct Result<T, E> {partial struct Task
{
	public Result.Ok<T>.Task ok() => new(Tok());
	async ValueTask<Result.Ok<T>> Tok() => (await _task).ok();

	public Result.Err<E>.Task err() => new(Terr());
	async ValueTask<Result.Err<E>> Terr() => (await _task).err();


	public Result<U, E>.Task map<U>(Func<T, U> f) => new(Tmap(f));
	async ValueTask<Result<U, E>> Tmap<U>(Func<T, U> f) => (await _task).map(f);

	public Result<T, F>.Task map_err<F>(Func<E, F> f) => new(Tmap_err(f));
	async ValueTask<Result<T, F>> Tmap_err<F>(Func<E, F> f) => (await _task).map_err(f);


	public Task inspect(Action<T> f) => new(Tinspect(f));
	async ValueTask<Result<T, E>> Tinspect(Action<T> f) => (await _task).inspect(f);

	public Task inspect_err(Action<E> f) => new(Tinspect_err(f));
	async ValueTask<Result<T, E>> Tinspect_err(Action<E> f) => (await _task).inspect_err(f);


	/// <inheritdoc cref="Result{T, E}.map{U}(U, Func{T, U})"/>
	public async ValueTask<U> map<U>(U or, Func<T, U> f) => (await _task).map(or, f);

	/// <inheritdoc cref="Result{T, E}.map{U}(Func{U}, Func{T, U})"/>
	public async ValueTask<U> map<U>(Func<U> or_else, Func<T, U> f) => (await _task).map(or_else, f);


	public Result<U, E>.Task and<U>(Result<U, E> other) => new(Tand(other));
	async ValueTask<Result<U, E>> Tand<U>(Result<U, E> other) => (await _task).and(other);

	public Result<U, E>.Task and_then<U>(Func<T, Result<U, E>> f) => new(Tand_then(f));
	async ValueTask<Result<U, E>> Tand_then<U>(Func<T, Result<U, E>> f) => (await _task).and_then(f);


	public Result.Err<E>.Task and(Result.Err<E> other) => new(Tand_err(other));
	async ValueTask<Result.Err<E>> Tand_err(Result.Err<E> other) => (await _task).and(other);

	public Result.Err<E>.Task and_then(Func<T, Result.Err<E>> f) => new(Tand_then_err(f));
	async ValueTask<Result.Err<E>> Tand_then_err(Func<T, Result.Err<E>> f) => (await _task).and_then(f);

	public Result.Err<E>.Task and_then(Func<T, ValueTask<Result.Err<E>>> f) => new(Tand_then_err(f));
	async ValueTask<Result.Err<E>> Tand_then_err(Func<T, ValueTask<Result.Err<E>>> f) => 
		await (await _task).Tand_then(f);


	public Result<T, F>.Task or<F>(Result<T, F> other) => new(Tor(other));
	async ValueTask<Result<T, F>> Tor<F>(Result<T, F> other) => (await _task).or(other);

	public Result<T, F>.Task or_else<F>(Func<E, Result<T, F>> f) => new(Tor_else(f));
	async ValueTask<Result<T, F>> Tor_else<F>(Func<E, Result<T, F>> f) => (await _task).or_else(f);

	public Result<T, F>.Task or_else<F>(Func<E, ValueTask<Result<T, F>>> f) => new(Tor_else(f));
	async ValueTask<Result<T, F>> Tor_else<F>(Func<E, ValueTask<Result<T, F>>> f) => 
		await (await _task).Tor_else(f);


	public Result.Ok<T>.Task or(Result.Ok<T> other) => new(Tor_ok(other));
	async ValueTask<Result.Ok<T>> Tor_ok(Result.Ok<T> other) => (await _task).or(other);

	public Result.Ok<T>.Task or_else(Func<E, Result.Ok<T>> f) => new(Tor_else_ok(f));
	async ValueTask<Result.Ok<T>> Tor_else_ok(Func<E, Result.Ok<T>> f) => (await _task).or_else(f);

	public Result.Ok<T>.Task or_else(Func<E, ValueTask<Result.Ok<T>>> f) => new(Tor_else_ok(f));
	async ValueTask<Result.Ok<T>> Tor_else_ok(Func<E, ValueTask<Result.Ok<T>>> f) => 
		await (await _task).or_else(f);
}}