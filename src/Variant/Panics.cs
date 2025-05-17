namespace KiriLib.Variant;

public sealed class UnwrapException : InvalidOperationException
{
	internal UnwrapException(string obj) : base($"bad unwrap: {obj}") {}
}

public sealed class ExpectException : InvalidOperationException
{
	internal ExpectException(string msg) : base($"bad expect: {msg}") {}
}