namespace Kirisoup.Lib.Variant;

partial struct Result<T, E> 
{
	public bool IsOk() => _isOk;
	public bool IsErr() => !_isOk;

	public bool IsOkAnd(Func<T, bool> predicate) => _isOk && predicate(_ok);
	public bool IsErrOr(Func<T, bool> predicate) => !_isOk || predicate(_ok);
	public bool IsOkOr(Func<E, bool> predicate) => _isOk || predicate(_err);
	public bool IsErrAnd(Func<E, bool> predicate) => !_isOk && predicate(_err);

	/// <param name="ok">
	/// is valid only if method returned true, 
	/// otherwise zeroed or garbage data might be returned. 
	/// </param>
	public bool IsOk(out T ok) {
		ok = _ok;
		return _isOk;
	}

	/// <param name="err">
	/// is valid only if method returned true, 
	/// otherwise zeroed or garbage data might be returned. 
	/// </param>
	public bool IsErr(out E err) {
		err = _err;
		return !_isOk;
	}

	public T Unwrap() => _isOk ? _ok : throw new UnwrapException($"Err({_err.ToStringNullable()})");
	public E UnwrapErr() => !_isOk ? _err : throw new UnwrapException($"Ok({_ok.ToStringNullable()})");

	public T Expect(string msg) => _isOk ? _ok : throw new ExpectException(msg);
	public E ExpectErr(string msg) => !_isOk ? _err : throw new ExpectException(msg);

	public T OkOr(T @default) => _isOk ? _ok : @default;
	public T OkOr(Func<T> @else) => _isOk ? _ok : @else();
}
