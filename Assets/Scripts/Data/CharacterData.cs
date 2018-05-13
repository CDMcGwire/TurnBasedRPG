using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/Data", order = 1)]
public class CharacterData : ScriptableObject {
	[SerializeField]
	private Character character;
	public Character Character { get { return new Character(character); } }
}
