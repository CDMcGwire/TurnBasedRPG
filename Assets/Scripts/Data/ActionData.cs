using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Action", menuName = "Combat/Action", order = 1)]
public class ActionData : ScriptableObject {
	[SerializeField]
	private Action action;
	public string ActionName { get { return action.Name; } }
	[SerializeField]
	private Targeter targeter;
	public Targeter Targeter { get { return targeter; } }


	public Action GetAction(Combatant actor, Target target) {
		return new Action(actor, target, action);
	}
}