using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void ActionSelectDelegate(ActionData actionData);

public class ActionSelectionMenu : MonoBehaviour {
	private Pool<ActionSelector> selectorPool;
	private List<ActionSelector> activeSelectors;

	private Follower follower;

	private void Awake() {
		selectorPool = new Pool<ActionSelector>(GetComponentsInChildren<ActionSelector>(true));
		activeSelectors = new List<ActionSelector>(selectorPool.Capacity);
		follower = GetComponent<Follower>();
	}

	public void Populate(Combatant combatant, ActionSelectDelegate selectionDelegate) {
		var actions = combatant.Character.KnownActions;
		CheckValidActionCount(actions);
		ResizeActiveButtons(actions);

		for (var i = 0; i < activeSelectors.Count; i++) {
			activeSelectors[i].Populate(actions[i], selectionDelegate);
		}

		if (follower != null) follower.SetTarget(combatant.Sprite.transform);
	}

	private void ResizeActiveButtons(List<ActionData> actions) {
		if (actions.Count > activeSelectors.Count) {
			var limit = Math.Min(actions.Count, selectorPool.Capacity);
			for (var i = activeSelectors.Count; i < limit; i++) {
				activeSelectors.Add(selectorPool.GetNext());
				activeSelectors[i].gameObject.SetActive(true);
			}
		}
		else if (actions.Count < activeSelectors.Count) {
			for (var i = activeSelectors.Count - 1; i > actions.Count; i--) {
				selectorPool.Return(activeSelectors[i]);
			}
		}
	}

	private void CheckValidActionCount(List<ActionData> actions) {
		if (actions.Count > selectorPool.Capacity) {
			Debug.LogWarningFormat("Attempted to list more actions [{}] than {} has capacity to display",
					actions.Count, name);
		}
	}
}
