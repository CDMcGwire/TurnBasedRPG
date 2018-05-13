using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Event/OnStart Event Trigger")]
public class OnStartEventTrigger : MonoBehaviour {

	[SerializeField]
	UnityEvent OnStart;
	
	void Start () {
		OnStart.Invoke();
	}
}
