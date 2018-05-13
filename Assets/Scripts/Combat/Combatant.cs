using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour {
	[SerializeField]
	private Character character;
	public Character Character { get { return character; } } // Make private?
	[SerializeField]
	private string faction;
	public string Faction { get { return faction; } }

	public CombatSprite Sprite { get; private set; }
	public CombatController Controller { get; private set; }

	// Initialization methods
	public void Initialize(Character character, string faction) {
		this.character = character;
		name = character.Name + " - Combatant";
		this.faction = faction;
		Sprite = Instantiate(character.CombatSprite, gameObject.transform);
		Sprite.name = character.Name + " - Sprite";
		Controller = Instantiate(character.CombatController, gameObject.transform);
		Controller.name = character.Name + " - Controller";
		Controller.Actor = this;
	}
	
	public List<Combatant> Spawn(List<Character> characters, string faction, Transform parent) {
		var combatants = new List<Combatant>();
		foreach (var character in characters) {
			combatants.Add(Spawn(character, faction, parent));
		}
		return combatants;
	}
	public Combatant Spawn(Character character, string faction, Transform parent) {
		var combatant = Instantiate(this, parent);
		combatant.Initialize(character, faction);
		return combatant;
	}

	// Interface methods !! TODO: nix these and move up. No point redirecting like this.
	public Action ChooseAction() {
		return Controller.ChooseAction();
	}
	public EDefenseType Damage(int amount, EDamageType type) {
		var response = Character.Defenses.GetDefense(type);
		switch (response) {
			case EDefenseType.Normal:
				Character.DamageHealth(amount);
				break;
			case EDefenseType.Resisted:
				break;
			case EDefenseType.Weakness:
				break;
			case EDefenseType.Nullified:
				break;
			case EDefenseType.Reflected:
				break;
			default:
				break;
		}

		if (!Character.Alive) Sprite.gameObject.SetActive(false);

		return response;
	}
	public void Heal(int amount) {

	}
	public void Recover(int amount) {

	}
}