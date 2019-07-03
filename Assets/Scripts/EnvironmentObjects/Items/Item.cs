public abstract class Item : BaseObject, IPoolableObject {
	public abstract void PutInPool();

	public virtual void Disable() => gameObject.SetActive(false);

	public virtual void Enable() => gameObject.SetActive(true);

	public override void Move(PointNode newNode) {
		base.Move(newNode);
		ChangeNodeState(newNode, NodeStates.OccupiedByItem);
	}
}

public interface IPoolableObject {
	void Disable();
	void Enable();
}