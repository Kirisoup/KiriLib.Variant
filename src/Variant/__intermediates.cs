using System.ComponentModel;

namespace KiriLib.Variant;

[Obsolete("types here are not suppsed to be directly interacted with. ")]
[EditorBrowsable(EditorBrowsableState.Never)] 
public static partial class __intermediates
{
	public readonly ref struct None 
	{
		public Option<T> _<T>() => this; // _<> turbowhale
	}

	public readonly ref struct Ok<T>
	{
		internal readonly T _value;
		internal Ok(T value) => _value = value;

		public Result<T, E> _<E>() => this;
		public KiriLib.Variant.Ok<T> _() => this;
	}

	public readonly ref struct Err<E>
	{
		internal readonly E _value;
		internal Err(E value) => _value = value;

		public Result<T, E> _<T>() => this;
		public KiriLib.Variant.Err<E> _() => this;
	}

	public readonly ref struct Ok() 
	{
		public KiriLib.Variant.Err<E> _<E>() => this;
	}

	public readonly ref struct Err() 
	{
		public KiriLib.Variant.Ok<T> _<T>() => this;
	}
}