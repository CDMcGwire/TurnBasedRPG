using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "EncounterData", menuName = "Encounter/Data", order = 1)]
public class Encounter : ScriptableObject {
	[SerializeField]
	List<EncounterGroup> groups = new List<EncounterGroup>();

	Dictionary<String, List<ICombatant>> factionMap = new Dictionary<string, List<ICombatant>>();
	TurnCounter<ICombatant> turnCounter;
	
	public List<ICombatant> GetAllCombatants() {
		var result = new List<ICombatant>();
		foreach (var faction in factionMap.Values)
			result.AddRange(faction);

		return result;
	}

	public ICombatant CurrentCombatant { get { return turnCounter.CurrentItem; } }
	public int CurrentRound { get { return turnCounter.CurrentRound; } }
	
	// Initializer
	void OnEnable() {
		foreach (var group in groups) {
			foreach (var combatant in group.Combatants) {
				AddToFactionMap(combatant, group.Faction);
			}
		}

		turnCounter = new TurnCounter<ICombatant>(
			GetAllCombatants(),
			(a, b) => b.Speed.CompareTo(a.Speed)
		);
	}

	// Private methods
	private void AddToFactionMap(ICombatant combatant, string faction) {
		if (!factionMap.ContainsKey(faction)) {
			factionMap.Add(faction, new List<ICombatant>());
		}
		factionMap[faction].Add(combatant);
	}

	// Interface Methods
	public void EndTurn() { turnCounter.EndTurn(); }

	public void AddCombatant(ICombatant combatant, string faction) {
		AddToFactionMap(combatant, faction);
		turnCounter.Add(combatant);
	}
	public bool RemoveCombatant(ICombatant combatant, string faction) {
		return turnCounter.Remove(combatant);
	}
}
