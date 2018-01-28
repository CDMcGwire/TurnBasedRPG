using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EncounterManager : MonoBehaviour {
	Encounter activeEncounter;
	
	[SerializeField]
	UnityEvent OnTurnEnd;
	[SerializeField]
	UnityEvent OnRoundEnd;
}
