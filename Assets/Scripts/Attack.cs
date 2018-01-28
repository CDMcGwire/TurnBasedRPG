using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Data/Attack", order = 1)]
public class Attack : ScriptableObject {
	[SerializeField]
	string attackName = "default";
	public string AttackName { get { return attackName; } }
}