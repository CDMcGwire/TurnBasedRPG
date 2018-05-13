using UnityEngine;

public abstract class CombatController : MonoBehaviour {
	public Combatant Actor { get; set; }
	public abstract void Setup(FactionMap factionMap);
	public abstract void Cleanup();
	public abstract Action ChooseAction();
}
