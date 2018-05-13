/*	
 *	Generic state machine logic
 *	Designed to provide a logic flow for the state machine.
 *	Expects owning object to provide initial state, and for
 *	each state to provide the following state.
 */

using UnityEngine;
public class StateEngine<T> {
	public abstract class State : State<T> { // Internalized subclass of state
		public abstract State Run(T owner);
	}

	private readonly T owner;

	private State currentState;
	public string CurrentStateName { get { return currentState.Name; } }
	
	private double LastUpdate { get; set; }
	public double StateDuration { get; private set; }

	public StateEngine(T owner, State initialState) {
		this.owner = owner;
		Debug.Log("Starting State Engine");
		SetState(initialState);
	}
	
	public void Run() {
		StateDuration += Time.time - LastUpdate; // Track state time
		LastUpdate = Time.time;

		var nextState = currentState.Run(owner);
		if (nextState != null) {
			SetState(nextState);
		}
	}

	public void SetState(State nextState) {
		if (currentState != null) {
			Debug.LogFormat("Exiting state {0}", currentState.Name);
			currentState.Exit(owner);
		}
		currentState = nextState;
		StateDuration = 0.0;
		if (currentState != null) {
			Debug.LogFormat("Entering state {0}", currentState.Name);
			currentState.Enter(owner);
		}
	}
}
