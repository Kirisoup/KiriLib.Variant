namespace Kirisoup.Lib.Variant;

partial struct Result<T, E> 
{
	public bool IsOk => _var.IsExpected;
	public bool IsErr => !_var.IsExpected;

	public bool IsOkAnd(Func<T, bool> predicate) => IsOk && predicate(_ok);
	public bool IsErrOr(Func<T, bool> predicate) => IsErr || predicate(_ok);
	public bool IsOkOr(Func<E, bool> predicate) => IsOk || predicate(_err);
	public bool IsErrAnd(Func<E, bool> predicate) => IsErr && predicate(_err);
	
	// alternative to pattarn matching, 
	// since csharp dont allow user defined type to overload pattern matching

	/// <param name="ok">
	/// is valid only if method returned true, 
	/// otherwise zeroed or garbage data might be returned. 
	/// </param>
	public bool IsOkThen(out T ok) {
		ok = _ok;
		return IsOk;
	}

	/// <param name="err">
	/// is valid only if method returned true, 
	/// otherwise zeroed or garbage data might be returned. 
	/// </param>
	public bool IsErrThen(out E err) {
		err = _err;
		return IsErr;
	}

	/// <param name="ok">
	/// is valid only if method returned true, 
	/// otherwise zeroed or garbage data might be returned. 
	/// </param>
	/// <param name="err">
	/// is valid only if method returned false, 
	/// otherwise zeroed or garbage data might be returned. 
	/// </param>
	public bool IsOkThenOr(out T ok, out E err) {
		(ok, err) = (_ok, _err);
		return IsOk;
	}
}
