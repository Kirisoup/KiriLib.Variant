namespace Kirisoup.Lib.Variant;

partial struct Option<T> {partial struct Task
{
	public Result.Ok<T>.Task ok() => new(Tok());
	async ValueTask<Result.Ok<T>> Tok() => (await _task).ok();
	
	public Result.Err<T>.Task err() => new(Terr());
	async ValueTask<Result.Err<T>> Terr() => (await _task).err();


	/// <inheritdoc cref="Option{T}.ok{E}(E)"/>
	public Result<T, E>.Task ok<E>(E or) => new(Tok(or));
	async ValueTask<Result<T, E>> Tok<E>(E or) => (await _task).ok(or);

	/// <inheritdoc cref="Option{T}.ok{E}(Func{E})"/>
	public Result<T, E>.Task ok<E>(Func<E> or_else) => new(Tok_else(or_else));
	async ValueTask<Result<T, E>> Tok_else<E>(Func<E> or_else) => (await _task).ok(or_else);


	/// <inheritdoc cref="Option{T}.err{t}(t)"/>
	public Result<t, T>.Task err<t>(t or) => new(Terr(or));
	async ValueTask<Result<t, T>> Terr<t>(t or) => (await _task).err(or);

	/// <inheritdoc cref="Option{T}.err{t}(Func{t})"/>
	public Result<t, T>.Task err<t>(Func<t> or_else) => new(Terr_else(or_else));
	async ValueTask<Result<t, T>> Terr_else<t>(Func<t> or_else) => (await _task).err(or_else);


	public Option<U>.Task map<U>(Func<T, U> f) => new(Tmap(f));
	async ValueTask<Option<U>> Tmap<U>(Func<T, U> f) => (await _task).map(f);

	public Task inspect(Action<T> f) => new(Tinspect(f));
	async ValueTask<Option<T>> Tinspect(Action<T> f) => (await _task).inspect(f);


	/// <inheritdoc cref="Option{T}.map{U}(U, Func{T, U})"/>
	public async ValueTask<U> map<U>(U or, Func<T, U> f) => (await _task).map(or, f);

	/// <inheritdoc cref="Option{T}.map{U}(Func{U}, Func{T, U})"/>
	public async ValueTask<U> map<U>(Func<U> or_else, Func<T, U> f) => (await _task).map(or_else, f);


	public Option<U>.Task and<U>(Option<U> other) => new(Tand(other));
	async ValueTask<Option<U>> Tand<U>(Option<U> other) => (await _task).and(other);

	public Option<U>.Task and_then<U>(Func<T, Option<U>> f) => new(Tand_then(f));
	async ValueTask<Option<U>> Tand_then<U>(Func<T, Option<U>> f) => (await _task).and_then(f);

	public Option<U>.Task and_then<U>(Func<T, ValueTask<Option<U>>> f) => new(Tand_then(f));
	async ValueTask<Option<U>> Tand_then<U>(Func<T, ValueTask<Option<U>>> f) => 
		await (await _task).Tand_then(f);


	public Task or(Option<T> other) => new(Tor(other));
	async ValueTask<Option<T>> Tor(Option<T> other) => (await _task).or(other);

	public Task or_else(Func<Option<T>> f) => new(Tor_else(f));
	async ValueTask<Option<T>> Tor_else(Func<Option<T>> f) => (await _task).or_else(f);

	public Task or_else(Func<ValueTask<Option<T>>> f) => new(Tor_else(f));
	async ValueTask<Option<T>> Tor_else(Func<ValueTask<Option<T>>> f) => 
		await (await _task).Tor_else(f);


	public Option<U>.Task cast<U>() => new(Tcast<U>());
	async ValueTask<Option<U>> Tcast<U>() => (await _task).cast<U>();	
}}