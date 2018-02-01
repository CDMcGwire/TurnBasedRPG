using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EncounterGroup", menuName = "Encounter/Group", order = 1)]
public class EncounterGroup : ScriptableObject {
	[SerializeField]
	string faction = "Enemy";
	[SerializeField]
	List<Character> combatants = new List<Character>();

	public string Faction { get { return faction; } }
	public ICombatant[] Combatants { get { return combatants.ToArray(); } }
}