using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Arena : MonoBehaviour {
	public abstract void Populate(FactionMap combatants);
	public abstract void ReorderRegion(string faction, List<Combatant> combatants);
}
