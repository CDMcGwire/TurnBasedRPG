using UnityEngine;

public class EncounterManager : MonoBehaviour {
	private const string MISSING_MANAGER_ERROR = "No Encounter Manager present in the scene: Cannot start encounter!!";

	private static EncounterManager instance;

	[SerializeField]
	private Encounter defaultEncounterPrefab;

	[SerializeField]
	[HideInInspector]
	private Encounter activeEncounter;

	// MonoBehaviour Methods
	private void Awake() {
		if (instance == null) instance = this;
	}
	private void OnEnable() {
		if (activeEncounter == null) gameObject.SetActive(false);
	}

	// Static Interface
	public static void StartEncounter(EncounterData data) {
		Debug.Assert(instance.defaultEncounterPrefab != null, "Encounter manager has no default Encounter Prefab");
		StartEncounter(data, instance.defaultEncounterPrefab);
	}
	public static void StartEncounter(EncounterData data, Encounter encounterPrefab) {
		if (instance.activeEncounter != null) {
			Debug.LogWarning("An encounter is already running");
			return;
		}

		instance.activeEncounter = Instantiate(encounterPrefab, instance.transform);
		instance.activeEncounter.Populate(data.Roster, data.Arena);
		instance.gameObject.SetActive(true);
	}

	public static void EndEncounter() {
		if (instance.activeEncounter != null) Destroy(instance.activeEncounter.gameObject);
		instance.gameObject.SetActive(false);
	}
}
