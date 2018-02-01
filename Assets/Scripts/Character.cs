using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/Data", order = 1)]
public class Character : ScriptableObject, ICombatant {
	[SerializeField]
	private string characterName = "default";
	[SerializeField]
	private CombatSprite combatSprite;

	[SerializeField]
	private bool isAlive = true;
	[SerializeField]
	private bool isVulnerable = false;
	[SerializeField]
	private int speed = 1;
	
	[SerializeField]
	private List<Attack> attacks = new List<Attack>();

	public string Name { get { return characterName; } }
	public bool Alive { get { return isAlive; } }
	public bool Vulnerable { get { return isVulnerable; } }
	public int Speed { get { return speed; } }

	// Comparison logic
	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) return false;
		return Name == (obj as Character).Name;
	}
	public bool Equals(Character other) {
		return Name == other.Name;
	}
	public override int GetHashCode() {
		return characterName.GetHashCode();
	}
	public static bool operator ==(Character lhs, Character rhs) {
		return lhs.Equals(rhs);
	}
	public static bool operator !=(Character lhs, Character rhs) {
		return !lhs.Equals(rhs);
	}
}
