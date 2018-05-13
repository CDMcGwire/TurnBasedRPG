using System;
using System.Collections.Generic;
using UnityEngine;

// Need to make encounter state saveable
[Serializable]
public class Encounter : MonoBehaviour {
	public const string HERO_FACTION = "Hero";
	public const string ENEMY_FACTION = "Enemy";
	private FactionMap combatants; // Define serialization method

	[SerializeField]
	private Combatant baseCombatant;

	[SerializeField]
	[Header("Populated by default")]
	private Arena arena;
	public Arena Arena { get { return arena; } }

	[SerializeField]
	private TurnCounter<Combatant> turnCounter;
	public Combatant CurrentCombatant { get { return turnCounter.CurrentItem; } }
	public int CurrentRound { get { return turnCounter.CurrentRound; } }

	private AnimationQueue animationQueue;

	private StateEngine<Encounter> stateEngine;

	private string winner = null;

	// Initializer
	public void Populate(EncounterRoster roster, Arena arenaPrefab) { // Eventually should add 'Script'
		animationQueue = GetComponentInChildren<AnimationQueue>();

		combatants = GenerateFactionMap(roster);

		turnCounter = new TurnCounter<Combatant>(
			(a, b) => b.Character.Speed.Current.CompareTo(a.Character.Speed.Current)
		);
		turnCounter.Add(combatants[HERO_FACTION]);
		turnCounter.Add(combatants[ENEMY_FACTION]);
		turnCounter.Reset();

		arena = Instantiate(arenaPrefab, transform);
		arena.Populate(combatants);

		stateEngine = new StateEngine<Encounter>(this, new SetupState());
	}

	// MonoBehaviours
	private void Update() {
		stateEngine.Run();
	}

	private void OnDestroy() {
		EncounterManager.EndEncounter();
	}

	// Interface Methods
	public void End() {
		stateEngine.SetState(new EndState());
	}

	public void AddCharacter(Character character, string faction) {
		var combatant = baseCombatant.Spawn(character, faction, transform);
		combatants.Add(faction, combatant);
		turnCounter.Add(combatant);
	}
	public void RemoveCombatant(Combatant combatant) {
		combatants[combatant.Faction].Remove(combatant);
		turnCounter.Remove(combatant);
		Destroy(combatant.gameObject);
	}
	
	private FactionMap GenerateFactionMap(EncounterRoster roster) {
		var factionMap = new FactionMap {
			{ HERO_FACTION, baseCombatant.Spawn(roster.Heroes, HERO_FACTION, transform) },
			{ ENEMY_FACTION, baseCombatant.Spawn(roster.Enemies, ENEMY_FACTION, transform) }
		};
		return factionMap;
	}

	private string CheckWinner() { // TODO: Probably should make this less obtuse
		var livingFactions = new List<string>();
		foreach (var faction in combatants.Keys) {
			if (combatants.ContainsLiving(faction)) livingFactions.Add(faction);
		}
		if (livingFactions.Count > 1) return null;
		else if (livingFactions.Count == 1) return livingFactions[0];
		else return "Everyone Died!";
	}

	/* * * * * * * * * * * * * * * * *  STATES  * * * * * * * * * * * * * * * * */

	private class SetupState : StateEngine<Encounter>.State {
		public override string Name { get { return "Setup"; } }

		public override StateEngine<Encounter>.State Run(Encounter owner) {
			if (owner.animationQueue.Blocking) return null;
			else return new ChooseState();
		}
	}

	private class ChooseState : StateEngine<Encounter>.State {
		public override string Name { get { return "Choose"; } }

		public override void Enter(Encounter owner) {
			Debug.LogFormat("Combatant {0} is beginning their turn", owner.CurrentCombatant.Character.Name);
			owner.CurrentCombatant.Controller.Setup(owner.combatants);
		}
		public override void Exit(Encounter owner) {
			owner.CurrentCombatant.Controller.Cleanup();
			owner.turnCounter.EndTurn();
		}

		public override StateEngine<Encounter>.State Run(Encounter owner) {
			if (!owner.CurrentCombatant.Character.Alive) return new ResolveState();

			var action = owner.CurrentCombatant.ChooseAction();
			if (action != null) {
				action.Apply();
				return new ResolveState();
			}

			return null;
		}
	}

	private class ResolveState : StateEngine<Encounter>.State {
		public override string Name { get { return "Resolve"; } }

		public override StateEngine<Encounter>.State Run(Encounter owner) {
			if (owner.animationQueue.Blocking) return null;
			else {
				var winner = owner.CheckWinner();
				if (winner == null) return new ChooseState();
				else {
					owner.winner = winner;
					return new EndState();
				}
			}
		}
	}

	//private class NarrativeState : StateEngine<Encounter>.State {
	//	public override string Name { get { return "Narrative"; } }
	//
	//	public override StateEngine<Encounter>.State Run(Encounter owner) { }
	//}

	private class EndState : StateEngine<Encounter>.State {
		public override string Name { get { return "End of Combat"; } }

		public override void Enter(Encounter owner) {
			//owner.arena.Close();
		}

		public override StateEngine<Encounter>.State Run(Encounter owner) {
			Debug.LogFormat("Winner: {0}", owner.winner);
			EncounterManager.EndEncounter();
			//if (!owner.animationQueue.Blocking) owner.finished = true;
			return null;
		}
	}
}
