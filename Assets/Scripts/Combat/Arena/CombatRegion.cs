using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CombatRegion : MonoBehaviour {
	const string COUNT_WARNING = "Attempted to set Combat Region with more sprites [{}] than it has positions [{}]";

	[SerializeField]
	private List<Transform> positions = new List<Transform>();
	[SerializeField]
	private List<CombatSprite> sprites = new List<CombatSprite>();

	[SerializeField]
	private float repositionTIme = 0.3f;

	public int MaxSize { get { return positions.Count; } }
	public int CurrentSize { get { return sprites.Count; } }

	private void Start() {
		Debug.Assert(positions.Count > 0, "Combat Region exists without markers: Add via inspector");
	}

	public void Populate(List<Combatant> combatants) {
		if (combatants.Count > positions.Count) {
			Debug.LogWarning(string.Format(COUNT_WARNING, combatants.Count, positions.Count));
		}
		sprites.Clear();

		var i = 0;
		while (i < combatants.Count && i < positions.Count) {
			combatants[i].Sprite.transform.SetParent(positions[i]);
			combatants[i].Sprite.transform.localPosition = Vector3.zero;
			sprites.Add(combatants[i].Sprite);
			i++;
		}
	}

	public void Reorder(List<Combatant> newOrder) {
		if (newOrder.Count> positions.Count) {
			Debug.LogWarning(string.Format(COUNT_WARNING, newOrder.Count, positions.Count));
			return;
		}

		sprites.Clear();
		foreach (var combatant in newOrder) sprites.Add(combatant.Sprite);

		for (var i = 0; i < sprites.Count; i++) {
			sprites[i].transform.SetParent(positions[i]);
			sprites[i].SetTargetPosition(positions[i].position, repositionTIme);
		}
	}


}
