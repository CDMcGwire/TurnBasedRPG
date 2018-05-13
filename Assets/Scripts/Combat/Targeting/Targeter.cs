using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Targeter", menuName = "Combat/Targeter", order = 1)]
public class Targeter : ScriptableObject {
	[SerializeField]
	private TargetSelector targetSelector;
	public TargetSelector SelectorPrefab { get { return targetSelector; } }

	[SerializeField]
	private EGroupType targetingRange;
	[SerializeField]
	[Header("Ignored on 'Self' target")]
	private ETargetType validTargets;
	[SerializeField]
	private bool ignoreUntargetable = false;

	public List<Target> GetTargets(Combatant actor, FactionMap combatants) {
		switch (targetingRange) {
			case EGroupType.Self:
				return GetSelfTarget(actor);
			case EGroupType.Single:
				return GetSingleTargets(actor, combatants);
			case EGroupType.Group:
				return GetGroupTargets(actor, combatants);
			case EGroupType.All:
				return GetAllTarget(combatants);
			default:
				return new List<Target>();
		}
	}

	private List<Target> GetSelfTarget(Combatant actor) {
		return new List<Target> { new Target(actor) };
	}

	private List<Target> GetSingleTargets(Combatant actor, FactionMap combatants) {
		var targets = new List<Target>();
		switch (validTargets) {
			case ETargetType.Enemies:
				foreach (var faction in combatants)
					if (faction.Key != actor.Faction)
						foreach (var combatant in faction.Value)
							AddSingleTarget(combatant, targets);
				break;
			case ETargetType.Allies:
				foreach (var combatant in combatants[actor.Faction])
					AddSingleTarget(combatant, targets);
				break;
			case ETargetType.Both:
				foreach (var faction in combatants.Values)
					foreach (var combatant in faction)
						AddSingleTarget(combatant, targets);
				break;
			default:
				break;
		}
		return targets;
	}

	private List<Target> GetGroupTargets(Combatant actor, FactionMap combatants) {
		var targets = new List<Target>();
		switch (validTargets) {
			case ETargetType.Enemies:
				foreach (var faction in combatants)
					if (faction.Key != actor.Faction)
						AddGroupTarget(faction.Value, targets);
				break;
			case ETargetType.Allies:
				AddGroupTarget(combatants[actor.Faction], targets);
				break;
			case ETargetType.Both:
				foreach (var faction in combatants.Values)
					AddGroupTarget(faction, targets);
				break;
			default:
				break;
		}
		return targets;
	}

	private List<Target> GetAllTarget(FactionMap combatants) {
		var targetableCombatants = new List<Combatant>();

		foreach (var faction in combatants.Values)
			foreach (var combatant in faction)
				if (CanTarget(combatant))
					targetableCombatants.Add(combatant);

		return new List<Target> { new Target(targetableCombatants) };
	}

	private void AddSingleTarget(Combatant combatant, List<Target> list) {
		if (CanTarget(combatant)) list.Add(new Target(combatant));
	}
	private void AddGroupTarget(List<Combatant> combatants, List<Target> list) {
		var targetableCombatants = new List<Combatant>();

		foreach (var combatant in combatants)
			if (CanTarget(combatant))
				targetableCombatants.Add(combatant);

		if (targetableCombatants.Count > 0)
			list.Add(new Target(targetableCombatants));
	}

	private bool CanTarget(Combatant combatant) {
		return combatant.Character.Alive && (ignoreUntargetable || combatant.Character.Targetable);
	}
}

public enum EGroupType {
	Self,
	Single,
	Group,
	All
}
public enum ETargetType {
	Enemies,
	Allies,
	Both
}