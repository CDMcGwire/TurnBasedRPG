using UnityEngine;

public class Watcher : MonoBehaviour {
	[SerializeField]
	private bool watchMainCamera = true;

	[SerializeField]
	private Transform target;

	[SerializeField]
	private bool targetX = true;
	[SerializeField]
	private bool targetY = true;
	[SerializeField]
	private bool targetZ = true;

	[SerializeField]
	private bool lookAway = false;
	[SerializeField]
	private bool localSpace = true;

	private void Start() {
		if (watchMainCamera) {
			target = Camera.main.transform;
		}
		else if (target == null) {
			Debug.LogWarning(name + " has a Watcher component but no target - Disabling component");
			enabled = false;
		}
		Watch();
	}

	private void Update() { Watch(); }

	private void Watch() {
		var targetPostion = new Vector3(
			(targetX) ? target.position.x : transform.position.x,
			(targetY) ? target.position.y : transform.position.y,
			(targetZ) ? target.position.z : transform.position.z
		);
		transform.LookAt(targetPostion, localSpace ? transform.up : target.up);
		if (lookAway) transform.Rotate(0, 180, 0, Space.Self);
	}
}
