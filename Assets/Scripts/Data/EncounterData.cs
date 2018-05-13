using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "EncounterData", menuName = "Encounter/Data", order = 1)]
public class EncounterData : ScriptableObject {
	[SerializeField]
	private EncounterRoster roster;
	public EncounterRoster Roster { get { return roster; } }

	[SerializeField]
	private Arena arena;
	public Arena Arena { get { return arena; } }
}
