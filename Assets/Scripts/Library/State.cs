public abstract class State<T> {
	public abstract string Name { get; }
	public virtual void Enter(T owner) { }
	public virtual void Exit(T owner) { }
}
