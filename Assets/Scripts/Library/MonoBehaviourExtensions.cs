using UnityEngine;

public static class MonoBehaviourExtensions {
	public static bool isPrefab(this MonoBehaviour monoBehaviour) {
		return monoBehaviour.gameObject.scene.rootCount == 0;
	}
}
