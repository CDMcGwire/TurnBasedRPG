using System.Collections.Generic;
using UnityEngine;

public class TestArena : MonoBehaviour {
	public EncounterRoster roster;
	public Arena arena;
	public Combatant combatantPrefab;

	private FactionMap factionMap;
	private System.Random random = new System.Random();

	private void Start() {
		var heroes = combatantPrefab.Spawn(roster.Heroes, Encounter.HERO_FACTION, transform);
		var enemies = combatantPrefab.Spawn(roster.Enemies, Encounter.ENEMY_FACTION, transform);
		factionMap = new FactionMap {
			{ Encounter.HERO_FACTION, heroes },
			{ Encounter.ENEMY_FACTION, enemies }
		};

		arena.Populate(factionMap);

		InvokeRepeating("ShuffleHeroes", 1.0f, 1f);
		InvokeRepeating("ShuffleEnemies", 1.5f, 1f);
	}

	public void ShuffleEnemies() {
		var newOrder = new List<Combatant>(factionMap[Encounter.ENEMY_FACTION]);
		newOrder.Shuffle(random);
		arena.ReorderRegion(Encounter.ENEMY_FACTION, newOrder);
	}

	public void ShuffleHeroes() {
		var newOrder = new List<Combatant>(factionMap[Encounter.HERO_FACTION]);
		newOrder.Shuffle(random);
		arena.ReorderRegion(Encounter.HERO_FACTION, newOrder);
	}
}
