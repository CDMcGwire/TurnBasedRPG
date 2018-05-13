using System.Collections.Generic;

public interface IWeighted {
	int Weight { get; }
}

public static class MathFunctions {
	public static T WeightedRandomItem<T>(List<T> list) where T : IWeighted {
		var total = 0;
		foreach (var item in list) total += item.Weight;
		var randomValue = UnityEngine.Random.Range(0, total);

		foreach (var item in list) {
			if (item.Weight < randomValue) {
				return item;
			}
			randomValue -= item.Weight;
		}
		return list[0];
	}
}
