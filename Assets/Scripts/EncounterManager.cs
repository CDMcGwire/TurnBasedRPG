using UnityEngine;
using UnityEngine.Events;

public class EncounterManager : MonoBehaviour {
	Encounter activeEncounter;

	[SerializeField]
	UnityEvent OnEncounterStart;
	[SerializeField]
	UnityEvent OnEncounterEnd;
	[SerializeField]
	UnityEvent OnTurnEnd;
	[SerializeField]
	UnityEvent OnRoundEnd;

	public void StartEncounter(Encounter data) {
		activeEncounter = data;
		OnEncounterStart.Invoke();
	}

	public void EndEncounter() {
		activeEncounter = null;
		OnEncounterEnd.Invoke();
	}
}
