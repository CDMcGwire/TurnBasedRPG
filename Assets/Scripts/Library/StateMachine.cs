public class StateMachine<T> {
	protected readonly T owner;

	protected State<T> currentState;
	public string CurrentStateName { get { return currentState.Name; } }

	public StateMachine(T owner, State<T> initialState) {
		this.owner = owner;
		SetState(initialState);
	}

	public void SetState(State<T> nextState) {
		if (currentState != null) currentState.Exit(owner);
		currentState = nextState;
		if (currentState != null) currentState.Enter(owner);
	}
}
