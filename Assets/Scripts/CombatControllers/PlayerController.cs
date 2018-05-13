using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CombatController {
	[SerializeField]
	private ActionSelectionMenu actionMenu;
	[SerializeField]
	private TargetSelectionMenu targetMenu;
	[SerializeField]
	private Button cancelButton;

	private StateMachine<MonoBehaviour> stateMachine;
	private ActiveObjectState actionMenuState;
	private ActiveObjectState targetMenuState;

	private FactionMap factionMap;

	private Action selectedAction = null;

	public override void Setup(FactionMap factionMap) {
		this.factionMap = factionMap;
		actionMenu.Populate(
			Actor,
			actionData => SelectTarget(actionData)
		);
		stateMachine.SetState(actionMenuState);
	}

	public override void Cleanup() {
		factionMap = null;
		selectedAction = null;
		stateMachine.SetState(null);
	}

	public override Action ChooseAction() {
		return selectedAction;
	}

	private void Start() {
		Debug.AssertFormat(
			actionMenu != null,
			"No action menu specified for player controller '{0}'",
			name);
		actionMenu.gameObject.SetActive(false);
		Debug.AssertFormat(
			targetMenu != null,
			"No selector pool specified for player controller '{0}'",
			name);
		targetMenu.gameObject.SetActive(false);
		Debug.AssertFormat(
			cancelButton != null,
			"No cancel button specified for player controller '{0}'",
			name);
		cancelButton.gameObject.SetActive(false);

		cancelButton.onClick.AddListener(() => stateMachine.SetState(actionMenuState));

		actionMenuState = new ActiveObjectState("Action Menu", actionMenu.gameObject);
		targetMenuState = new ActiveObjectState("Target Menu", targetMenu.gameObject, cancelButton.gameObject);
		stateMachine = new StateMachine<MonoBehaviour>(this, null);
	}

	private void SelectTarget(ActionData actionData) {
		targetMenu.Populate(
			actionData,
			Actor,
			factionMap,
			target => selectedAction = actionData.GetAction(Actor, target));

		stateMachine.SetState(targetMenuState);
	}
}
