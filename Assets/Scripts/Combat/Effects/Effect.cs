using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Effect", menuName = "Combat/Effects/TestEffect", order = 1)]
public class Effect : ScriptableObject {
	public void Apply(Combatant actor, Target target) {
		foreach (var combatant in target.Targets) {
			combatant.Damage(1, EDamageType.Force);
			Debug.LogFormat("{0} was hit for 1 Damage! {0}'s current health = {1}", combatant.Character.Name, combatant.Character.Health.Current);
		}
		// Get algorithm from notes later
	}
}
