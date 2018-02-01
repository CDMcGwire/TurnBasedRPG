using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Attack", menuName = "Combat/Attack", order = 1)]
public class Attack : ScriptableObject {
	[SerializeField]
	string attackName = "default";
	public string AttackName { get { return attackName; } }
}