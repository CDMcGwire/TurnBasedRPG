using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StandardArena : Arena {
	private const string NO_HERO_LAYOUT = "This arena does not contain a layout for the number of heroes provided [{}]";
	private const string NO_ENEMY_LAYOUT = "This arena does not contain a layout for the number of enemies provided [{}]";

	[SerializeField]
	[HideInInspector]
	private CombatRegion heroRegion;

	[SerializeField]
	[HideInInspector]
	private CombatRegion enemyRegion;

	[SerializeField]
	private List<CombatRegion> heroRegionPrefabs = new List<CombatRegion>(4);
	[SerializeField]
	private List<CombatRegion> enemyRegionPrefabs = new List<CombatRegion>(6);

	private void Awake() {
		Debug.Assert(heroRegionPrefabs.Count > 0, "Hero regions list on " + name + " is empty: Please insert at least one value");
		Debug.Assert(!heroRegionPrefabs.Contains(null), "Hero regions list on " + name + " contains a null value");
		Debug.Assert(enemyRegionPrefabs.Count > 0, "Enemy regions list on " + name + " is empty: Please insert at least one value");
		Debug.Assert(!enemyRegionPrefabs.Contains(null), "Enemy regions list on " + name + " contains a null value");
	}

	public override void Populate(FactionMap combatants) {
		if (heroRegion != null || enemyRegion != null) {
			Debug.LogWarning("Should not be populating an arena more than once: Use an 'add' method");
			return;
		}

		var heroes = combatants[Encounter.HERO_FACTION];
		var enemies = combatants[Encounter.ENEMY_FACTION];

		heroRegion = Instantiate(SelectRegion(heroRegionPrefabs, heroes.Count), transform);
		enemyRegion = Instantiate(SelectRegion(enemyRegionPrefabs, enemies.Count), transform);

		heroRegion.Populate(heroes);
		enemyRegion.Populate(enemies);
	}

	public override void ReorderRegion(string faction, List<Combatant> combatants) {
		if (faction == "Hero") heroRegion.Reorder(combatants);
		else if (faction == "Enemy") enemyRegion.Reorder(combatants);
		else {
			Debug.LogWarning("Tried to reorder region using faction " + faction + " but no match was found");
		}
	}

	private CombatRegion SelectRegion(List<CombatRegion> regions, int groupSize) {
		regions.Sort((a, b) => a.MaxSize.CompareTo(b.MaxSize));
		foreach (var region in regions) if (region.MaxSize >= groupSize) return region;
		return null;
	}
}
