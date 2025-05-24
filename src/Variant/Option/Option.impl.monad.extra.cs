namespace Kirisoup.Lib.Variant;

partial struct Option<T> 
{
	public Option<T> filter(Func<T, bool> predicate) => (!_isSome || predicate(_some)) 
		? this 
		: Option.None<T>();

	// lol
	public Option<T> xor(Option<T> other) => (_isSome, other._isSome) switch {
		var (a, b) when a == b => Option.None<T>(),
		var (a, _) => a ? this : other
	};

	public Option<(T, U)> zip<U>(Option<U> other) => (!_isSome || !other._isSome)
		? Option.None<(T, U)>()
		: Option.Some((_some, other._some));

	/// <remarks>
	/// <c>.zip(other, with: _)</c>
	/// </remarks>
	public Option<V> zip<U, V>(Option<U> other, Func<T, U, V> with) => (!_isSome || !other._isSome)
		? Option.None<V>()
		: Option.Some(with(_some, other._some));
}

public static class OptionTUImpl {
extension <T, U> (Option<(T, U)> self)  
{
	public (Option<T>, Option<U>) unzip() => self._isSome
		? (Option.Some(self._some.Item1), Option.Some(self._some.Item2))
		: (Option.None<T>(), Option.None<U>());
}}

public static class OptionResultImpl {
extension <T, E> (Option<Result<T, E>> self) 
{
	public Result<Option<T>, E> transpose() {
		if (!self._isSome)
			return Result.Ok<Option<T>, E>(Option.None<T>());
		var result = self._some;
		return result._isOk
			? Result.Ok<Option<T>, E>(Option.Some(result._ok))
			: Result.Err<Option<T>, E>(result._err);
	}
}}

public static class OptionOptionImpl {
extension <T> (Option<Option<T>> self) 
{
	public Option<T> flatten() => new(self._isSome && self._some._isSome, self._some._some);
}}