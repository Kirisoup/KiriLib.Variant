namespace KiriLib.Variant;

partial struct Option<T> 
{
	public Option<T> filter(Func<T, bool> predicate) => (IsNone || predicate(_some)) 
		? this 
		: Option.None<T>();

	// lol
	public Option<T> xor(Option<T> other) => (IsSome, other.IsSome) switch {
		var (a, b) when a == b => Option.None<T>(),
		var (a, _) => a ? this : other
	};

	public Option<(T, U)> zip<U>(Option<U> other) => (IsNone || other.IsNone)
		? Option.None<(T, U)>()
		: Option.Some((_some, other._some));

	/// <remarks>
	/// <c>.zip(other, with: _)</c>
	/// </remarks>
	public Option<V> zip<U, V>(Option<U> other, Func<T, U, V> with) => (IsNone || other.IsNone)
		? Option.None<V>()
		: Option.Some(with(_some, other._some));
}

public static class OptionTUImpl {
extension <T, U> (Option<(T, U)> self)  
{
	public (Option<T>, Option<U>) unzip() => self.IsSomeThen(out var pair)
		? (Option.Some(pair.Item1), Option.Some(pair.Item2))
		: (Option.None<T>(), Option.None<U>());
}}

public static class OptionResultImpl {
extension <T, E> (Option<Result<T, E>> self) 
{
	public Result<Option<T>, E> transpose() => self
		.map (res => res.map(f: Option.Some))
		.some (or: Result.Ok(Option.None<T>()));
}}

public static class OptionOptionImpl {
extension <T> (Option<Option<T>> self) 
{
	public Option<T> flatten() => self.some(or: Option.None<T>());
}}