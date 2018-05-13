using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Action {
	private Combatant actor;
	private Target target;

	[SerializeField]
	private string name;
	public string Name { get { return name; } }
	[SerializeField]
	private List<Effect> effects;
	public List<Effect> Effects { get { return effects; } }

	public Action(
		Combatant actor, 
		Target target,
		String name,
		List<Effect> effects) {

		this.actor = actor;
		this.target = target;
		this.name = name;
		this.effects = new List<Effect>(effects);
	}
	public Action(Combatant actor, Target target, Action other) 
		: this(actor, target, other.name, other.effects) { }

	public void Apply() { foreach (var effect in effects) effect.Apply(actor, target); }
}
