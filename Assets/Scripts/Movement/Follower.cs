using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
	[SerializeField]
	private List<Transform> targets;
	public List<Transform> Targets {
		get { return targets; }
		set {
			targets = value;
			enabled = true;
		}
	}

	private Vector3 originalScale = Vector3.one;
	public float Scale {
		set {
			transform.localScale = new Vector3(
				value * originalScale.x, 
				value * originalScale.y, 
				value * originalScale.z);
		}
	}

	private void Start() {
		if (targets == null) enabled = false;
		originalScale = transform.localScale;
		Follow();
	}

	private void Update() { Follow(); }

	private void Follow() {
		if (targets.Count < 1) return;
		var total = Vector3.zero;
		foreach (var transform in targets) total += transform.position;
		transform.position = total / targets.Count;
	}

	public void SetTarget(Transform target) {
		targets.Clear();
		targets.Add(target);
	}
}
