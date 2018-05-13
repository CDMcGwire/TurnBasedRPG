using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Should have a reset function to return transitive stats back after a fight
 */

[Serializable]
public class Character {
	private const string FORM_BAD_PREFAB = "Tried setting the {} prefab reference for {} to a non-prefab";

	[SerializeField]
	private bool persistent = false;
	public bool Persistent { get { return persistent; } }

	[SerializeField]
	private string characterName = "Default";
	public string Name { get { return characterName; } }

	[SerializeField]
	private BoundedInt health = new BoundedInt(3,3);
	public BoundedInt Health { get { return health; } }
	public void DamageHealth(int amount) {
		health.Current -= amount;
		if (health.Current < 1) isAlive = false;
	}
	[SerializeField]
	private BoundedInt poise = new BoundedInt(2,1);
	public BoundedInt Poise { get { return poise; } }
	[SerializeField]
	private BoundedInt speed = new BoundedInt(1,1);
	public BoundedInt Speed { get { return speed; } }

	[SerializeField]
	private DefenseMap defenses = new DefenseMap();
	public DefenseMap Defenses { get { return defenses; } }

	[SerializeField]
	private bool isAlive = true;
	public bool Alive {
		get { return isAlive; }
		set { isAlive = value; }
	}
	[SerializeField]
	private bool staggered = false;
	public bool Staggered {
		get { return staggered; }
		set { staggered = value; }
	}
	
	[SerializeField]
	private List<ActionData> actions = new List<ActionData>();
	public List<ActionData> KnownActions {
		get { return new List<ActionData>(actions); }
	}

	[SerializeField]
	private CombatSprite combatSprite;
	public CombatSprite CombatSprite {
		get { return combatSprite; }
		set {
			if (value.gameObject.scene.rootCount > 0) combatSprite = value;
			else Debug.LogWarningFormat(FORM_BAD_PREFAB, "Combat Sprite", Name);
		}
	}
	[SerializeField]
	private CombatController combatController;
	public CombatController CombatController {
		get { return combatController; }
		set {
			if (value.gameObject.scene.rootCount > 0) combatController = value;
			else Debug.LogWarningFormat(FORM_BAD_PREFAB, "Controller", Name);
		}
	}

	[SerializeField]
	private bool targetable = true;
	public bool Targetable {
		get { return targetable; }
		set { targetable = value; }
	}

	// Constructors
	public Character() { }
	public Character(Character other) {
		characterName = other.Name;
		health = other.Health;
		poise = other.Poise;
		speed = other.Speed;
		defenses = other.Defenses;
		combatSprite = other.combatSprite;
		combatController = other.combatController;
		actions = new List<ActionData>(other.actions);
	}

	// Comparison logic
	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) return false;
		return Equals(obj as Character);
	}
	public bool Equals(Character other) {
		return Name == other.Name;
	}
	public override int GetHashCode() {
		return base.GetHashCode();
	}
	public static bool operator ==(Character lhs, Character rhs) {
		return lhs.Equals(rhs);
	}
	public static bool operator !=(Character lhs, Character rhs) {
		return !lhs.Equals(rhs);
	}

	// Interface methods
	public bool AddAction(ActionData action) {
		throw new NotImplementedException();
	}
}
