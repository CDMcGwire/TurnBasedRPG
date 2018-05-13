using UnityEngine;
using UnityEngine.UI;

public class ActionSelector : MonoBehaviour {
	[SerializeField]
	private Button button;
	[SerializeField]
	private Text text;

	internal void Populate(ActionData actionData, ActionSelectDelegate selectionDelegate) {
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() => selectionDelegate(actionData));
		text.text = actionData.ActionName;
	}
}
