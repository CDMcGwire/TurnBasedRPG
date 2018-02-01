using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Event/OnClickOut Event Trigger")]
public class OnClickOutTrigger : MonoBehaviour {

	[SerializeField]
	RectTransform targetElement;
	[SerializeField]
	UnityEvent OnClickOut;

	RectTransform rect;

	void Start() {
		rect = (targetElement) ? targetElement : GetComponent<RectTransform>();
	}

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			bool outside = !RectTransformUtility.RectangleContainsScreenPoint(
					rect,
					Input.mousePosition,
					null
				);
			if (outside) OnClickOut.Invoke();
		}
	}
}
