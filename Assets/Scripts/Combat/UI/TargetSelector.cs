using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelector : MonoBehaviour {
	[SerializeField]
	private Button button;

	[SerializeField]
	private Follower followComponent;

	private void OnValidate() {
		if (button == null) {
			Debug.LogWarning("Selector has no button reference");
		}
	}

	public void Populate(Target target, TargetSelectDelegate selectionDelegate) {
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() => selectionDelegate(target));
		if (followComponent != null) {
			followComponent.Targets = new List<Transform>(target.TargetTransforms);
			followComponent.Scale = target.Scale;
		}
	}
}
