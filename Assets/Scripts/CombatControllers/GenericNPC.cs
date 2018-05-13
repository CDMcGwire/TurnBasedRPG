public class GenericNPC : CombatController {
	private FactionMap factionMap;

	public override void Setup(FactionMap factionMap) {
		this.factionMap = factionMap;
	}

	public override void Cleanup() {
		factionMap = null;
	}

	public override Action ChooseAction() {
		var actionData = Actor.Character.KnownActions.RandomItem();
		var target = actionData.Targeter.GetTargets(Actor, factionMap).RandomItem();
		return actionData.GetAction(Actor, target);
	}
}
