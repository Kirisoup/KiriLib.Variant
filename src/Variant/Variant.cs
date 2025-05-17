namespace KiriLib.Variant;

internal readonly struct Variant
{
	readonly byte _value;
	Variant(byte value) => _value = value;

	internal static Variant Expected => new(255);
	internal static Variant Unexpected => new(254);

	internal byte IsExpectedByte => (byte)checked(_value - 254); // panic if _value < 254
	internal bool IsExpected => IsExpectedByte > 0;
	internal Variant Invert() => new((byte)(-IsExpectedByte + 255));

	[Obsolete("", true)] public Variant() => throw new NotSupportedException();
}