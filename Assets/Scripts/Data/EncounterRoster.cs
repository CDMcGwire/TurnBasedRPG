using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "EncounterRoster", menuName = "Encounter/Roster", order = 1)]
public class EncounterRoster : ScriptableObject {
	[SerializeField]
	private List<RosterGroup> enemyGroups = new List<RosterGroup>();

	[SerializeField]
	private List<CharacterData> heroGroup = new List<CharacterData>();
	[SerializeField]
	private bool replaceParty = false;

	public List<Character> Enemies {
		get {
			var enemyData = MathFunctions.WeightedRandomItem(enemyGroups).Characters;
			var enemies = new List<Character>();
			foreach (var data in enemyData) enemies.Add(data.Character);
			return enemies;
		}
	}

	public List<Character> Heroes {
		get {
			if (!replaceParty) return SaveFile.PlayerData.Party.ActiveRoster;
			var heroes = new List<Character>();
			foreach (var data in heroGroup) heroes.Add(data.Character);
			return heroes;
		}
	}
}

[Serializable]
public struct RosterGroup : IWeighted {
	[SerializeField]
	private List<CharacterData> characters;
	[SerializeField]
	int weight;

	public List<CharacterData> Characters {
		get { return characters; }
		set { characters = value; }
	}
	public int Weight {
		get { return weight; }
		set { weight = value; }
	}
}