using System;
using System.Collections.Generic;

public class Encounter {
	Dictionary<String, List<ICombatant>> factionMap 
		= new Dictionary<string, List<ICombatant>>();
	TurnCounter<ICombatant> turnCounter;

	// Properties
	public List<ICombatant> Combatants {
		get {
			var result = new List<ICombatant>();
			foreach (var faction in factionMap.Values)
				result.AddRange(faction);

			return result;
		}
	}

	public ICombatant CurrentCombatant { get { return turnCounter.CurrentItem; } }
	public int CurrentRound { get { return turnCounter.CurrentRound; } }

	// Constructors
	public Encounter(List<ICombatant> combatants) {
		foreach (var combatant in combatants)
			AddToFactionMap(combatant);

		turnCounter = new TurnCounter<ICombatant>(
			combatants,
			(a, b) => b.Speed.CompareTo(a.Speed)
		);
	}

	// Private methods
	private void AddToFactionMap(ICombatant combatant) {
		if (!factionMap.ContainsKey(combatant.Faction)) {
			factionMap.Add(combatant.Faction, new List<ICombatant>());
		}
		factionMap[combatant.Faction].Add(combatant);
	}

	// Interface Methods
	public void EndTurn() { turnCounter.EndTurn(); }

	public void AddCombatant(ICombatant combatant) {
		AddToFactionMap(combatant);
		turnCounter.Add(combatant);
	}
	public bool RemoveCombatant(ICombatant combatant) {
		return turnCounter.Remove(combatant);
	}
}
