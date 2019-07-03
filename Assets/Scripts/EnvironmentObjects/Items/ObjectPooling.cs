using System.Collections.Generic;

public class ObjectPooling<T> where T : IPoolableObject {
	private Stack<T> pool;
	public bool IsEmpty => pool.Count == 0;

	public ObjectPooling() => pool = new Stack<T>();

	public T Pop() {
		T obj = pool.Pop();
		obj.Enable();
		return obj; }

	public void Push(T pushableObject) {
		pushableObject.Disable();
		pool.Push(pushableObject);
	}
}