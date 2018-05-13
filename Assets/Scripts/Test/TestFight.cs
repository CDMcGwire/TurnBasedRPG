using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFight : MonoBehaviour {
	[SerializeField]
	private EncounterData encounterData;

	public void StartTestFight() {
		EncounterManager.StartEncounter(encounterData);
	}
}
