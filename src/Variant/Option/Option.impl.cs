namespace Kirisoup.Lib.Variant;

partial struct Option<T> 
{
	public bool IsSome() => _isSome;
	public bool IsNone() => !_isSome;

	public bool IsSomeAnd(Func<T, bool> predicate) => _isSome && predicate(_some);
	public bool IsNoneOr(Func<T, bool> predicate) => !_isSome || predicate(_some);

	/// <param name="some">
	/// is valid only if method returned true, 
	/// otherwise zeroed or garbage data might be returned. 
	/// </param>
	public bool IsSome(out T some) {
		some = _some;
		return _isSome;
	}

	public T Unwrap() => _isSome ? _some : throw new UnwrapException($"None<{typeof(T)}>");
	public T Expect(string msg) => _isSome ? _some : throw new ExpectException(msg);

	public T SomeOr(T @default) => _isSome ? _some : @default;
	public T SomeOr(Func<T> @else) => _isSome ? _some : @else();
}
