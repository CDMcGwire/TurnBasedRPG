using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void TargetSelectDelegate(Target target);

public class TargetSelectionMenu : MonoBehaviour {
	private LinkedList<TargetSelector> activeSelectors = new LinkedList<TargetSelector>();

	private void OnDisable() {
		foreach (var selector in activeSelectors) Destroy(selector.gameObject);
		activeSelectors.Clear();
	}

	public void Populate(ActionData actionData, Combatant owner, FactionMap factionMap, TargetSelectDelegate selectionDelegate) {
		var targets = actionData.Targeter.GetTargets(owner, factionMap);
		var selectorPrefab = actionData.Targeter.SelectorPrefab;

		foreach (var target in targets) {
			var selector = Instantiate(selectorPrefab, transform);
			selector.Populate(target, selectionDelegate);
			selector.transform.localScale = selectorPrefab.transform.localScale;
			activeSelectors.AddLast(selector);
		}
	}
}
