namespace Kirisoup.Lib.Variant;

internal static class ToStringNullableUtil 
{
	public static string ToStringNullable<T>(this T? self) => self?.ToString() ?? $"null<{typeof(T)}>";
}