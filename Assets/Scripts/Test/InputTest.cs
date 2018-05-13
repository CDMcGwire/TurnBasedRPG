using UnityEngine;

public class InputTest : MonoBehaviour {
	private new SpriteRenderer renderer;
	private Color oc;

	private void Start() {
		renderer = GetComponent<SpriteRenderer>();
		oc = renderer.color;
	}

	private void OnMouseDown() {
		renderer.color = Color.white;
	}
	private void OnMouseUp() {
		renderer.color = oc;
	}
}
