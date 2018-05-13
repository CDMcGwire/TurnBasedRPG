using UnityEngine;
using System.Collections;

public class CameraCacher : MonoBehaviour {
	[SerializeField]
	private Canvas canvas;

	void Start() {
		canvas.worldCamera = Camera.main;
	}
}
