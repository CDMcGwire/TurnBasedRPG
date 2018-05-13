using UnityEngine;

public class InputUITest : MonoBehaviour {
	public float speed = 0.0f;

	private void Update() {
		transform.Rotate(0, 0, speed * Time.deltaTime, Space.Self);
	}
}
