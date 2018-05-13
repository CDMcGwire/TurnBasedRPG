using UnityEngine;
using System.Collections;

[System.Serializable]
public struct DefenseMap {
	[SerializeField]
	private EDefenseType force;
	[SerializeField]
	private EDefenseType blade;
	[SerializeField]
	private EDefenseType ballistic;
	[SerializeField]
	private EDefenseType energy;
	[SerializeField]
	private EDefenseType chemical;
	[SerializeField]
	private EDefenseType composure;
	[SerializeField]
	private EDefenseType cognition;
	[SerializeField]
	private EDefenseType spirit;
	[SerializeField]
	private EDefenseType integrity;
	[SerializeField]
	private EDefenseType arcane;

	public EDefenseType GetDefense(EDamageType type) {
		switch (type) {
			case EDamageType.Force:
				return force;
			case EDamageType.Blade:
				return blade;
			case EDamageType.Ballistic:
				return ballistic;
			case EDamageType.Energy:
				return energy;
			case EDamageType.Chemical:
				return chemical;
			case EDamageType.Composure:
				return composure;
			case EDamageType.Cognition:
				return cognition;
			case EDamageType.Spirit:
				return spirit;
			case EDamageType.Integrity:
				return integrity;
			case EDamageType.Arcane:
				return arcane;
			default:
				return force;
		}
	}
	// Need a more adaptable way to handle this, but that will
	// likely require a custom editor and support for temp values
}
