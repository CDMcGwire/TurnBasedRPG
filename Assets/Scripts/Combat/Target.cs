using UnityEngine;
using System.Collections.Generic;

public class Target {
	public IReadOnlyList<Combatant> Targets { get; }
	public IReadOnlyList<Transform> TargetTransforms { get; }

	private const float baseScale = 1;
	public float Scale { get; }

	public Target(Combatant combatant) {
		Targets = new List<Combatant> { combatant };
		TargetTransforms = new List<Transform> { combatant.Sprite.transform };
		Scale = baseScale;
	}
	public Target(List<Combatant> combatants) {
		Targets = new List<Combatant>(combatants);

		var transforms = new List<Transform>();
		foreach (var target in Targets) transforms.Add(target.Sprite.transform);
		TargetTransforms = transforms;

		if (combatants.Count < 2) Scale = baseScale;
		else {
			var sum = Vector3.zero;
			foreach (var transform in TargetTransforms) sum += transform.position;
			var midpoint = sum / TargetTransforms.Count;
			var greatestDistance = 0.0f;
			foreach (var transform in TargetTransforms) {
				greatestDistance = Mathf.Max(greatestDistance, Vector3.Distance(midpoint, transform.position));
			}
			Scale = greatestDistance;
		}
	}
}
