using System.Collections;
using UnityEngine;

[System.Serializable]
public class CombatSprite : MonoBehaviour {
	private SpriteRenderer spriteRenderer;

	private Vector3 lerpFrom;
	private Vector3 lerpTo;
	private float lerpStart;
	private float lerpTime;
	private float lerpDistance;

	private void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void SetTargetPosition(Vector3 newPos, float time) {
		if (time < 0.00001) {
			transform.position = newPos;
			return;
		}

		lerpFrom = transform.position;
		lerpTo = newPos;
		lerpStart = Time.time;
		lerpTime = Mathf.Abs(time);
		lerpDistance = Vector3.Distance(lerpFrom, lerpTo);
		StartCoroutine(MoveToTarget());
	}

	private IEnumerator MoveToTarget() {
		while (Vector3.Distance(transform.position, lerpTo) > 0.001) {
			var progress = (Time.time - lerpStart) / lerpTime;
			transform.position = Vector3.Lerp(lerpFrom, lerpTo, progress);
			yield return null;
		}
		transform.position = lerpTo;
	}
}
