#pragma warning disable CS0618
using Kirisoup.Diagnostics.TypeUsageRules;

namespace Kirisoup.Lib.Variant;

partial class Result
{
	/// <summary>
	/// Representing either an ok result of <see cref="T" /> or an unit error. 
	/// </summary>
	/// <remarks>
	/// Similar to Result&lt;T, ()> in rust, but since csharp does not have language support for ZSTs, 
	/// it need to be its own type.
	/// </remarks>
	[NoDefault, NoNew]
	public readonly partial struct Ok<T>
	{
		internal readonly Option<T> _opt;
		internal Ok(Option<T> opt) => _opt = opt;

		public static implicit operator Ok<T>(Intermediates.Ok<T> ok) => new(Option.Some(ok._value));
		public static implicit operator Ok<T>(Intermediates.Err _) => new(Option.None<T>());
		public static implicit operator Ok<T>(T value) => new(Option.Some(value));
	}

	/// <summary>
	/// Representing either an unit ok result or an error of <see cref="E" />. 
	/// </summary>
	/// <remarks>
	/// Similar to Result&lt;(), E> in rust, but since csharp does not have language support for ZSTs, 
	/// it need to be its own type.
	/// </remarks>
	[NoDefault, NoNew]
	public readonly partial struct Err<E>
	{
		internal readonly Option<E> _opt;
		internal Err(Option<E> opt) => _opt = opt;

		public static implicit operator Err<E>(Intermediates.Ok _) => new(Option.None<E>());
		public static implicit operator Err<E>(Intermediates.Err<E> err) => new(Option.Some(err._value));
	}
}

partial class ResultCtorSugar {
extension (Result) {
	public static Result.Ok<T> Err<T>() => new(Option.None<T>());
	public static Result.Err<E> Ok<E>() => new(Option.None<E>());
	public static Intermediates.Err Err() => new();
	public static Intermediates.Ok Ok() => new();
}
}