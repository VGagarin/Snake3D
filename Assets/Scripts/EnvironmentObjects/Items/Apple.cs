public class Apple : Item {
	public ObjectPooling<Apple> pool { set; private get; }
	public override void PutInPool() => pool.Push(this);
}
