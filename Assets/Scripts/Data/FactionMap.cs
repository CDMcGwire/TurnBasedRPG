using System;
using System.Collections.Generic;

[Serializable]
public class FactionMap : Dictionary<string, List<Combatant>> {
	public FactionMap() : base() { }
	public FactionMap(IDictionary<string, List<Combatant>> dictionary) : base(dictionary) { }

	public void Add(String faction, Combatant combatant) {
		if (!ContainsKey(faction)) {
			Add(faction, new List<Combatant>());
		}
		this[faction].Add(combatant);
	}
	public void AddRange(String faction, List<Combatant> combatants) {
		if (!ContainsKey(faction)) {
			Add(faction, new List<Combatant>(combatants));
		}
		else {
			this[faction].AddRange(combatants);
		}
	}

	public bool ContainsLiving(string faction) {
		foreach (var combatant in this[faction])
			if (combatant.Character.Alive) return true;
		return false;
	}
}
