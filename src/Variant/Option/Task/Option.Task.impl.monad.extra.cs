namespace Kirisoup.Lib.Variant;

partial struct Option<T> {partial struct Task
{
	public Task filter(Func<T, bool> predicate) => new(Tfilter(predicate));
	async ValueTask<Option<T>> Tfilter(Func<T, bool> predicate) => (await _task).filter(predicate);

	public Task xor(Option<T> other) => new(Txor(other));
	async ValueTask<Option<T>> Txor(Option<T> other) => (await _task).xor(other);

	public Option<(T, U)>.Task zip<U>(Option<U> other) => new(Tzip(other));
	async ValueTask<Option<(T, U)>> Tzip<U>(Option<U> other) => (await _task).zip(other);

	public Option<V>.Task zip<U, V>(Option<U> other, Func<T, U, V> with) => new(Tzip(other, with));
	async ValueTask<Option<V>> Tzip<U, V>(Option<U> other, Func<T, U, V> with) => (await _task).zip(other, with);
}}

partial class OptionTUImpl {
extension <T, U> (Option<(T, U)>.Task self)  
{
	public async ValueTask<(Option<T>, Option<U>)> unzip() => (await self._task).unzip();
}}
