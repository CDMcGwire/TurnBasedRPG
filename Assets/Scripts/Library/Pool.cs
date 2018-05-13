using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour {
	private HashSet<T> active = new HashSet<T>();
	private Queue<T> inactive;

	public int Capacity { get; private set; }
	public int Remaining { get { return inactive.Count; } }
	public int ActiveCount { get { return active.Count; } }

	public Pool(int size, T baseObject, Transform parent) {
		Capacity = size;
		inactive = new Queue<T>(size);

		for (var i = 0; i < size; i++) {
			var item = Object.Instantiate(baseObject, parent);
			item.gameObject.SetActive(false);
			inactive.Enqueue(item);
		}
	}
	public Pool(List<T> items) {
		Capacity = items.Count;
		inactive = new Queue<T>(items);
		foreach (var item in inactive) item.gameObject.SetActive(false);
	}
	public Pool(T[] items) {
		Capacity = items.Length;
		inactive = new Queue<T>(items);
		foreach (var item in inactive) item.gameObject.SetActive(false);
	}

	public T GetNext() {
		var next = inactive.Dequeue();
		if (next != null) active.Add(next);
		return next;
	}

	public bool Return(T item) {
		var success = active.Remove(item);
		if (success) inactive.Enqueue(item);
		return success;
	}
	public void Reclaim() {
		foreach (var item in new List<T>(active)) {
			if (active.Remove(item)) {
				item.gameObject.SetActive(false);
				inactive.Enqueue(item);
			}
		}
	}
}
